using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TS3ClientQueryFramework;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using TS3ClientQueryFramework.TS3Models;

namespace TS3ScreenCapture
{
    public partial class Main : Form
    {
        TS3Client client = new TS3Client();
        Client me = null;
        int fpsCount = 0;
        int fp10sCount = 0;
        Thread captureThread = null;
        Thread receiveThread = null;
        string[] records = null;
        Regex startRegex = new Regex(@"START_BASE64_(\d*)");
        Regex contentRegex = new Regex(@"CONTENT_BASE64_(\d*)_");

        TcpListener tcpListener = new TcpListener(8888); //new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), 8888);
        TcpClient tcpClient = new TcpClient();
        Socket socket = null;

        public Main()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (client.Connect())
            {
                me = client.GetWhoami();
                comboBoxTab.DataSource = client.ServerConnectionHandlerList();
                comboBoxClient.DataSource = client.GetClientList();
                client.ClientNotifyRegister(client.CurrentHandlerId, Notifications.notifytextmessage);
                client.Notifier.OnTextMessage += Notifier_OnTextMessage;

                btnConnect.Enabled = false;
                comboBoxTab.Enabled = true;
                btnCapture.Enabled = true;
                comboBoxClient.Enabled = true;
                btnTcpCapture.Enabled = true;
                btnImageCapture.Enabled = true;
            }
            else
                MessageBox.Show("Error");
        }

        private void Notifier_OnTextMessage(TextMessage textMessage)
        {
            if (textMessage.Msg == null || !client.IsConnected())
                return;
            bool isValid = false;
            this.Invoke((MethodInvoker)delegate
            {
                if (comboBoxClient.SelectedItem != null && textMessage.Invoker.ClId == ((Client)comboBoxClient.SelectedItem).ClId)
                    isValid = true;
            });
            if (isValid)
            {
                if (startRegex.IsMatch(textMessage.Msg.Substring(0, textMessage.Msg.Length > 30 ? 30 : textMessage.Msg.Length)))
                {
                    Match match = startRegex.Match(textMessage.Msg);
                    if (match.Success)
                    {
                        records = new string[Convert.ToInt32(match.Groups[1].Value)];
                    }
                }
                else if (textMessage.Msg == "END_BASE64")
                {
                    try
                    {
                        string base64Image = string.Empty;
                        foreach (string s in records)
                        {
                            base64Image += s;
                        }

                        byte[] imagebytes = Convert.FromBase64String(base64Image);

                        MemoryStream ms = new MemoryStream(imagebytes);
                        Image image = Image.FromStream(ms);

                        this.Invoke((MethodInvoker)delegate
                        {
                            picCapture.Image = image;
                        });

                        fpsCount++;
                        fp10sCount++;
                    }
                    catch { }
                }
                else if (contentRegex.IsMatch(textMessage.Msg.Substring(0, textMessage.Msg.Length > 30 ? 30 : textMessage.Msg.Length)))
                {
                    Match match = contentRegex.Match(textMessage.Msg.Substring(0, textMessage.Msg.Length > 30 ? 30 : textMessage.Msg.Length));
                    if (match.Success)
                    {
                        try
                        {
                            records[Convert.ToInt32(match.Groups[1].Value)] = textMessage.Msg.Remove(0, match.Length);
                        }
                        catch { }
                    }
                }
                else if(textMessage.Msg == "TCP_SCREEN_CAPTURE_BEGIN")
                {
                    if (receiveThread == null)
                    {
                        TcpReceiveLoop();
                    }
                }
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (captureThread == null)
                CaptureLoop((int)numSpeed.Value, (int)numFactor.Value, (int)numQuallity.Value);
            else
            {
                captureThread.Abort();
                captureThread = null;
            }
        }

        private void CaptureImageLoop(Bitmap image, int scallFactor = 2, int quallity = 30)
        {
            captureThread = new Thread(delegate ()
            {
                int clid = me.ClId;
                this.Invoke((MethodInvoker)delegate
                {
                    clid = ((Client)comboBoxClient.SelectedItem).ClId;
                });
                Bitmap bImage = image; //Image

                if (quallity >= 0)
                    bImage = CompressImage(bImage, quallity);
                if (scallFactor > 1)
                    bImage = ResizeImage(bImage, Screen.PrimaryScreen.Bounds.Width / scallFactor, Screen.PrimaryScreen.Bounds.Height / scallFactor);

                this.Invoke((MethodInvoker)delegate
                {
                    picCapture.Image = (Image)bImage.Clone();
                });

                MemoryStream ms = new MemoryStream();
                bImage.Save(ms, ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();
                string base64 = Convert.ToBase64String(byteImage); //Get Base64
                    
                Queue<string> sends = new Queue<string>();
                int length = 1000;
                int splitsCount = 0;
                while (!string.IsNullOrEmpty(base64))
                {
                    if (base64.Length < length)
                        length = base64.Length;
                    string output = base64.Substring(0, length);
                    sends.Enqueue("CONTENT_BASE64_" + splitsCount + "_" + output);
                    base64 = base64.Remove(0, length);
                    splitsCount++;
                }

                client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, clid, "START_BASE64_" + splitsCount, true);

                foreach (string s in sends)
                {
                    client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, clid, s, true);
                }
                    
                client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, clid, "END_BASE64", true);

                client.ClearReadBuffer();

                captureThread = null;
            });
            captureThread.Start();
        }

        private void CaptureLoop(int speed = 1000, int scallFactor = 2, int quallity = 30)
        {
            captureThread = new Thread(delegate ()
            {
                int clid = me.ClId;
                this.Invoke((MethodInvoker)delegate
                {
                    clid = ((Client)comboBoxClient.SelectedItem).ClId;
                });
                while (true)
                {
                    Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                   Screen.PrimaryScreen.Bounds.Height,
                                   PixelFormat.Format32bppRgb); //Format32bppArgb

                    // Create a graphics object from the bitmap.
                    Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                    // Take the screenshot from the upper left corner to the right bottom corner.
                    gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                                Screen.PrimaryScreen.Bounds.Y,
                                                0,
                                                0,
                                                Screen.PrimaryScreen.Bounds.Size,
                                                CopyPixelOperation.SourceCopy);

                    Bitmap bImage = bmpScreenshot; //Image

                    if (quallity >= 0)
                        bImage = CompressImage(bImage, quallity);
                    if (scallFactor > 1)
                        bImage = ResizeImage(bmpScreenshot, Screen.PrimaryScreen.Bounds.Width / scallFactor, Screen.PrimaryScreen.Bounds.Height / scallFactor);

                    this.Invoke((MethodInvoker)delegate
                    {
                        picCapture.Image = (Image)bImage.Clone();
                    });

                    MemoryStream ms = new MemoryStream();
                    bImage.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();
                    string base64 = Convert.ToBase64String(byteImage); //Get Base64

                    Queue<string> sends = new Queue<string>();
                    int length = 1000;
                    int splitsCount = 0;
                    while (!string.IsNullOrEmpty(base64))
                    {
                        if (base64.Length < length)
                            length = base64.Length;
                        string output = base64.Substring(0, length);
                        sends.Enqueue("CONTENT_BASE64_" + splitsCount + "_" + output);
                        base64 = base64.Remove(0, length);
                        splitsCount++;
                    }

                    client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, clid, "START_BASE64_" + splitsCount, true);

                    foreach (string s in sends)
                    {
                        client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, clid, s, true);
                    }

                    client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, clid, "END_BASE64", true);

                    if (speed > 0)
                        Thread.Sleep(speed);
                    client.ClearReadBuffer();
                }
            });
            captureThread.Start();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            if (width == 0 || height == 0)
                return null;

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private static Bitmap CompressImage(Image sourceImage, int imageQuality)
        {
            try
            {
                //Create an ImageCodecInfo-object for the codec information
                ImageCodecInfo jpegCodec = null;

                //Set quality factor for compression
                EncoderParameter imageQualitysParameter = new EncoderParameter(
                            System.Drawing.Imaging.Encoder.Quality, imageQuality);

                //List all avaible codecs (system wide)
                ImageCodecInfo[] alleCodecs = ImageCodecInfo.GetImageEncoders();

                EncoderParameters codecParameter = new EncoderParameters(1);
                codecParameter.Param[0] = imageQualitysParameter;

                //Find and choose JPEG codec
                for (int i = 0; i < alleCodecs.Length; i++)
                {
                    if (alleCodecs[i].MimeType == "image/jpeg")
                    {
                        jpegCodec = alleCodecs[i];
                        break;
                    }
                }

                //Save compressed image
                MemoryStream ms = new MemoryStream();
                sourceImage.Save(ms, jpegCodec, codecParameter);
                return (Bitmap)Image.FromStream(ms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(captureThread != null)
                captureThread.Abort();
            captureThread = null;
            client.Disconnect();
            Environment.Exit(0);
        }

        private void timerFps_Tick(object sender, EventArgs e)
        {
            if (fpsCount == 0 && fp10sCount < 10 && fp10sCount > 0)
                lblFps.Text = "FPS: 0," + fp10sCount;
            else
                lblFps.Text = "FPS: " + fpsCount;
            fpsCount = 0;
        }

        private void timerFP10S_Tick(object sender, EventArgs e)
        {
            lblFP10S.Text = "FP10S: " + fp10sCount;
            fp10sCount = 0;
        }

        private void btnTcpCapture_Click(object sender, EventArgs e)
        {
            if (captureThread == null)
            {
                int clid = ((Client)comboBoxClient.SelectedItem).ClId;
                client.SendTextMessage(TextMessageTargetMode.TextMessageTarget_CLIENT, clid, "TCP_SCREEN_CAPTURE_BEGIN", true);
                Thread.Sleep(1000);
                TcpCaptureLoop((int)numSpeed.Value, (int)numFactor.Value, (int)numQuallity.Value);
            }
            else
            {
                captureThread.Abort();
                captureThread = null;
            }
        }

        private void TcpCaptureLoop(int speed = 1000, int scallFactor = 2, int quallity = 30)
        {
            captureThread = new Thread(delegate ()
            {
                int clid = me.ClId;
                this.Invoke((MethodInvoker)delegate
                {
                    clid = ((Client)comboBoxClient.SelectedItem).ClId;
                });
                while (true)
                {
                    Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                   Screen.PrimaryScreen.Bounds.Height,
                                   PixelFormat.Format32bppRgb); //Format32bppArgb

                    // Create a graphics object from the bitmap.
                    Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                    // Take the screenshot from the upper left corner to the right bottom corner.
                    gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                                Screen.PrimaryScreen.Bounds.Y,
                                                0,
                                                0,
                                                Screen.PrimaryScreen.Bounds.Size,
                                                CopyPixelOperation.SourceCopy);

                    Bitmap bImage = bmpScreenshot; //Image
                    if (quallity >= 0)
                        bImage = CompressImage(bImage, quallity);
                    if (scallFactor > 1)
                        bImage = ResizeImage(bmpScreenshot, Screen.PrimaryScreen.Bounds.Width / scallFactor, Screen.PrimaryScreen.Bounds.Height / scallFactor);

                    this.Invoke((MethodInvoker)delegate
                    {
                        picCapture.Image = (Image)bImage.Clone();
                    });

                    MemoryStream ms = new MemoryStream();
                    bImage.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();

                    tcpClient.Connect("127.0.0.1", 8888);
                    NetworkStream stream = tcpClient.GetStream();
                    stream.Write(byteImage, 0, byteImage.Length);
                }
            });
            captureThread.Start();
        }

        private void TcpReceiveLoop()
        {
            receiveThread = new Thread(delegate ()
            {
                tcpListener.Start();
                socket = tcpListener.AcceptSocket();
                byte[] buffer = new byte[1024];
                MemoryStream mem = null;
                while (true)
                {
                    if(socket.Receive(buffer) != 0)
                    {
                        if(mem == null)
                            mem = new MemoryStream();
                        mem.Write(buffer, 0, buffer.Length);
                    }
                    else if(mem != null)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            picCapture.Image = new Bitmap(mem);
                        });
                        mem.Close();
                        mem = null;
                    }
                }

            });
            receiveThread.Start();
        }

        private void comboBoxTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (client.CurrentHandlerId != (int)comboBoxTab.SelectedItem)
                client.Use((int)comboBoxTab.SelectedItem);
        }

        private void btnImageCapture_Click(object sender, EventArgs e)
        {
            if (captureThread == null)
            {
                if (openFileDialogImage.ShowDialog() == DialogResult.OK)
                {
                    CaptureImageLoop((Bitmap)Image.FromFile(openFileDialogImage.FileName), (int)numFactor.Value, (int)numQuallity.Value);
                }
            }
            else
            {
                captureThread.Abort();
                captureThread = null;
            }
        }
    }
}
