namespace TS3ScreenCapture
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.picCapture = new System.Windows.Forms.PictureBox();
            this.lblFps = new System.Windows.Forms.Label();
            this.timerFps = new System.Windows.Forms.Timer(this.components);
            this.lblFP10S = new System.Windows.Forms.Label();
            this.timerFP10S = new System.Windows.Forms.Timer(this.components);
            this.pnlController = new System.Windows.Forms.Panel();
            this.comboBoxTab = new System.Windows.Forms.ComboBox();
            this.btnTcpCapture = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numQuallity = new System.Windows.Forms.NumericUpDown();
            this.numFactor = new System.Windows.Forms.NumericUpDown();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.btnImageCapture = new System.Windows.Forms.Button();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).BeginInit();
            this.pnlController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuallity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 31);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCapture.Enabled = false;
            this.btnCapture.Location = new System.Drawing.Point(319, 50);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 33);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // picCapture
            // 
            this.picCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCapture.Location = new System.Drawing.Point(0, 86);
            this.picCapture.Name = "picCapture";
            this.picCapture.Size = new System.Drawing.Size(886, 457);
            this.picCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCapture.TabIndex = 2;
            this.picCapture.TabStop = false;
            // 
            // lblFps
            // 
            this.lblFps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFps.AutoSize = true;
            this.lblFps.Location = new System.Drawing.Point(820, 9);
            this.lblFps.Name = "lblFps";
            this.lblFps.Size = new System.Drawing.Size(39, 13);
            this.lblFps.TabIndex = 3;
            this.lblFps.Text = "FPS: 0";
            // 
            // timerFps
            // 
            this.timerFps.Enabled = true;
            this.timerFps.Interval = 1000;
            this.timerFps.Tick += new System.EventHandler(this.timerFps_Tick);
            // 
            // lblFP10S
            // 
            this.lblFP10S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFP10S.AutoSize = true;
            this.lblFP10S.Location = new System.Drawing.Point(820, 32);
            this.lblFP10S.Name = "lblFP10S";
            this.lblFP10S.Size = new System.Drawing.Size(51, 13);
            this.lblFP10S.TabIndex = 4;
            this.lblFP10S.Text = "FP10S: 0";
            // 
            // timerFP10S
            // 
            this.timerFP10S.Enabled = true;
            this.timerFP10S.Interval = 10000;
            this.timerFP10S.Tick += new System.EventHandler(this.timerFP10S_Tick);
            // 
            // pnlController
            // 
            this.pnlController.Controls.Add(this.btnImageCapture);
            this.pnlController.Controls.Add(this.comboBoxTab);
            this.pnlController.Controls.Add(this.btnTcpCapture);
            this.pnlController.Controls.Add(this.label3);
            this.pnlController.Controls.Add(this.label2);
            this.pnlController.Controls.Add(this.label1);
            this.pnlController.Controls.Add(this.numQuallity);
            this.pnlController.Controls.Add(this.numFactor);
            this.pnlController.Controls.Add(this.numSpeed);
            this.pnlController.Controls.Add(this.comboBoxClient);
            this.pnlController.Controls.Add(this.btnConnect);
            this.pnlController.Controls.Add(this.lblFP10S);
            this.pnlController.Controls.Add(this.btnCapture);
            this.pnlController.Controls.Add(this.lblFps);
            this.pnlController.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlController.Location = new System.Drawing.Point(0, 0);
            this.pnlController.Name = "pnlController";
            this.pnlController.Size = new System.Drawing.Size(886, 86);
            this.pnlController.TabIndex = 5;
            // 
            // comboBoxTab
            // 
            this.comboBoxTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTab.Enabled = false;
            this.comboBoxTab.FormattingEnabled = true;
            this.comboBoxTab.Location = new System.Drawing.Point(93, 19);
            this.comboBoxTab.Name = "comboBoxTab";
            this.comboBoxTab.Size = new System.Drawing.Size(41, 21);
            this.comboBoxTab.TabIndex = 14;
            this.comboBoxTab.SelectedIndexChanged += new System.EventHandler(this.comboBoxTab_SelectedIndexChanged);
            // 
            // btnTcpCapture
            // 
            this.btnTcpCapture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTcpCapture.Enabled = false;
            this.btnTcpCapture.Location = new System.Drawing.Point(400, 51);
            this.btnTcpCapture.Name = "btnTcpCapture";
            this.btnTcpCapture.Size = new System.Drawing.Size(75, 32);
            this.btnTcpCapture.TabIndex = 13;
            this.btnTcpCapture.Text = "TCP";
            this.btnTcpCapture.UseVisualStyleBackColor = true;
            this.btnTcpCapture.Click += new System.EventHandler(this.btnTcpCapture_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(706, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Speed:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(707, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Factor:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(703, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Quallity:";
            // 
            // numQuallity
            // 
            this.numQuallity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numQuallity.Location = new System.Drawing.Point(753, 59);
            this.numQuallity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numQuallity.Name = "numQuallity";
            this.numQuallity.Size = new System.Drawing.Size(61, 20);
            this.numQuallity.TabIndex = 9;
            this.numQuallity.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numFactor
            // 
            this.numFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numFactor.Location = new System.Drawing.Point(753, 33);
            this.numFactor.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numFactor.Name = "numFactor";
            this.numFactor.Size = new System.Drawing.Size(61, 20);
            this.numFactor.TabIndex = 7;
            this.numFactor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numSpeed
            // 
            this.numSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numSpeed.Location = new System.Drawing.Point(753, 7);
            this.numSpeed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(61, 20);
            this.numSpeed.TabIndex = 6;
            this.numSpeed.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClient.Enabled = false;
            this.comboBoxClient.FormattingEnabled = true;
            this.comboBoxClient.Location = new System.Drawing.Point(319, 19);
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(237, 21);
            this.comboBoxClient.TabIndex = 5;
            // 
            // btnImageCapture
            // 
            this.btnImageCapture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnImageCapture.Enabled = false;
            this.btnImageCapture.Location = new System.Drawing.Point(481, 51);
            this.btnImageCapture.Name = "btnImageCapture";
            this.btnImageCapture.Size = new System.Drawing.Size(75, 32);
            this.btnImageCapture.TabIndex = 15;
            this.btnImageCapture.Text = "Image";
            this.btnImageCapture.UseVisualStyleBackColor = true;
            this.btnImageCapture.Click += new System.EventHandler(this.btnImageCapture_Click);
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif;" +
    " *.png";
            this.openFileDialogImage.Title = "Please select an image file.";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 543);
            this.Controls.Add(this.picCapture);
            this.Controls.Add(this.pnlController);
            this.Name = "Main";
            this.Text = "TS3ScreenCapture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).EndInit();
            this.pnlController.ResumeLayout(false);
            this.pnlController.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuallity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.PictureBox picCapture;
        private System.Windows.Forms.Label lblFps;
        private System.Windows.Forms.Timer timerFps;
        private System.Windows.Forms.Label lblFP10S;
        private System.Windows.Forms.Timer timerFP10S;
        private System.Windows.Forms.Panel pnlController;
        private System.Windows.Forms.ComboBox comboBoxClient;
        private System.Windows.Forms.NumericUpDown numSpeed;
        private System.Windows.Forms.NumericUpDown numFactor;
        private System.Windows.Forms.NumericUpDown numQuallity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTcpCapture;
        private System.Windows.Forms.ComboBox comboBoxTab;
        private System.Windows.Forms.Button btnImageCapture;
        private System.Windows.Forms.OpenFileDialog openFileDialogImage;
    }
}

