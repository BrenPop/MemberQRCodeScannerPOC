using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Forms;
using QRCoder;
using ZXing;
using ZXing.Windows.Compatibility;

namespace MemberQRCodeScannerPOC
{
    partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.PictureBox pictureBoxWebcam;
        private System.Windows.Forms.Button buttonStartWebcam;
        private System.Windows.Forms.Button buttonStopWebcam;
        private System.Windows.Forms.ComboBox comboBoxDevices;

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private System.Windows.Forms.Timer qrCodeScanTimer;

        private void Form1_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                comboBoxDevices.Items.Add(device.Name);
            }
            comboBoxDevices.SelectedIndex = 0;
        }

        private void InitializeQrCodeTimer()
        {
            qrCodeScanTimer = new System.Windows.Forms.Timer();
            qrCodeScanTimer.Interval = 1000; // Set interval to 1 second
            qrCodeScanTimer.Tick += new EventHandler(detectQrCode_Tick);
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string text = textBoxInput.Text;
            if (!string.IsNullOrEmpty(text))
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                pictureBoxQRCode.Image = qrCodeImage;
            }
        }

        private void buttonStartWebcam_Click(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(videoDevices[comboBoxDevices.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
            qrCodeScanTimer.Start();
        }

        private void buttonStopWebcam_Click(object sender, EventArgs e)
        {
            videoSource.Stop();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBoxWebcam.Image = bitmap;
        }

        private void detectQrCode_Tick(object sender, EventArgs e)
        {
            if (pictureBoxWebcam.Image != null)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(pictureBoxWebcam.Image);
                    BarcodeReader reader = new BarcodeReader
                    {
                        AutoRotate = true,
                        TryInverted = true,
                        Options = new ZXing.Common.DecodingOptions
                        {
                            PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
                        }
                    };

                    LuminanceSource source = new ZXing.Windows.Compatibility.BitmapLuminanceSource(bitmap);
                    Result result = reader.Decode(source);

                    if (result != null)
                    {
                        qrCodeScanTimer.Stop();
                        MessageBox.Show(result.Text, "QR Code Content");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading QR code: {ex.Message}", "Exception");
                }
            }
            else
            {
                MessageBox.Show("No image available", "Error");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxQRCode = new PictureBox();
            buttonGenerate = new Button();
            textBoxInput = new TextBox();
            pictureBoxWebcam = new PictureBox();
            buttonStartWebcam = new Button();
            buttonStopWebcam = new Button();
            comboBoxDevices = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxQRCode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWebcam).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxQRCode
            // 
            pictureBoxQRCode.Location = new Point(12, 12);
            pictureBoxQRCode.Name = "pictureBoxQRCode";
            pictureBoxQRCode.Size = new Size(883, 687);
            pictureBoxQRCode.TabIndex = 0;
            pictureBoxQRCode.TabStop = false;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(901, 12);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(200, 23);
            buttonGenerate.TabIndex = 1;
            buttonGenerate.Text = "Generate";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(901, 41);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(200, 23);
            textBoxInput.TabIndex = 2;
            // 
            // pictureBoxWebcam
            // 
            pictureBoxWebcam.Location = new Point(1107, 12);
            pictureBoxWebcam.Name = "pictureBoxWebcam";
            pictureBoxWebcam.Size = new Size(883, 687);
            pictureBoxWebcam.TabIndex = 3;
            pictureBoxWebcam.TabStop = false;
            // 
            // buttonStartWebcam
            // 
            buttonStartWebcam.Location = new Point(1996, 41);
            buttonStartWebcam.Name = "buttonStartWebcam";
            buttonStartWebcam.Size = new Size(200, 23);
            buttonStartWebcam.TabIndex = 4;
            buttonStartWebcam.Text = "Start Webcam";
            buttonStartWebcam.UseVisualStyleBackColor = true;
            buttonStartWebcam.Click += buttonStartWebcam_Click;
            // 
            // buttonStopWebcam
            // 
            buttonStopWebcam.Location = new Point(1996, 99);
            buttonStopWebcam.Name = "buttonStopWebcam";
            buttonStopWebcam.Size = new Size(200, 23);
            buttonStopWebcam.TabIndex = 4;
            buttonStopWebcam.Text = "Stop Webcam";
            buttonStopWebcam.UseVisualStyleBackColor = true;
            buttonStopWebcam.Click += buttonStopWebcam_Click;
            // 
            // comboBoxDevices
            // 
            comboBoxDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDevices.FormattingEnabled = true;
            comboBoxDevices.Location = new Point(1996, 12);
            comboBoxDevices.Name = "comboBoxDevices";
            comboBoxDevices.Size = new Size(200, 23);
            comboBoxDevices.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2206, 714);
            Controls.Add(comboBoxDevices);
            Controls.Add(buttonStartWebcam);
            Controls.Add(buttonStopWebcam);
            Controls.Add(pictureBoxWebcam);
            Controls.Add(textBoxInput);
            Controls.Add(buttonGenerate);
            Controls.Add(pictureBoxQRCode);
            Name = "Form1";
            Text = "QR Code App";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxQRCode).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWebcam).EndInit();
            ResumeLayout(false);
            PerformLayout();
            InitializeQrCodeTimer();
        }

        #endregion
    }
}
