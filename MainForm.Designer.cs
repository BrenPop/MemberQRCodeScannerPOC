using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Forms;
using QRCoder;
using ZXing;
using ZXing.Windows.Compatibility;
using MemberQRCodeScannerPOC.Services;

namespace MemberQRCodeScannerPOC
{
    partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.PictureBox pictureBoxWebcam;
        private System.Windows.Forms.Button buttonStartWebcam;
        private System.Windows.Forms.Button buttonStopWebcam;
        private System.Windows.Forms.ComboBox comboBoxDevices;

        private QRCodeGenerationService qrCodeGenerationService;
        private WebcamService webcamService;
        private QRCodeScanningService qrCodeScanningService;

        private void Form1_Load(object sender, EventArgs e)
        {
            webcamService.LoadDevices();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string text = textBoxInput.Text;
            if (!string.IsNullOrEmpty(text))
            {
                qrCodeGenerationService.GenerateQRCode(text);
            }
        }

        private void buttonStartWebcam_Click(object sender, EventArgs e)
        {
            webcamService.StartWebcam();
            qrCodeScanningService.StartScanning();
        }

        private void buttonStopWebcam_Click(object sender, EventArgs e)
        {
            webcamService.StopWebcam();
            qrCodeScanningService.StopScanning();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            webcamService.Cleanup();
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
            pictureBoxQRCode.Location = new Point(12, 38);
            pictureBoxQRCode.Name = "pictureBoxQRCode";
            pictureBoxQRCode.Size = new Size(883, 748);
            pictureBoxQRCode.TabIndex = 0;
            pictureBoxQRCode.TabStop = false;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(218, 10);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(200, 23);
            buttonGenerate.TabIndex = 1;
            buttonGenerate.Text = "Add New Member";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 11);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(200, 23);
            textBoxInput.TabIndex = 2;
            // 
            // pictureBoxWebcam
            // 
            pictureBoxWebcam.Location = new Point(901, 38);
            pictureBoxWebcam.Name = "pictureBoxWebcam";
            pictureBoxWebcam.Size = new Size(883, 748);
            pictureBoxWebcam.TabIndex = 3;
            pictureBoxWebcam.TabStop = false;
            // 
            // buttonStartWebcam
            // 
            buttonStartWebcam.Location = new Point(1107, 9);
            buttonStartWebcam.Name = "buttonStartWebcam";
            buttonStartWebcam.Size = new Size(200, 23);
            buttonStartWebcam.TabIndex = 4;
            buttonStartWebcam.Text = "Start Webcam";
            buttonStartWebcam.UseVisualStyleBackColor = true;
            buttonStartWebcam.Click += buttonStartWebcam_Click;
            // 
            // buttonStopWebcam
            // 
            buttonStopWebcam.Location = new Point(1313, 9);
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
            comboBoxDevices.Location = new Point(901, 10);
            comboBoxDevices.Name = "comboBoxDevices";
            comboBoxDevices.Size = new Size(200, 23);
            comboBoxDevices.TabIndex = 6;
            // 
            // MainForm
            //
            qrCodeGenerationService = new QRCodeGenerationService(pictureBoxQRCode);
            webcamService = new WebcamService(pictureBoxWebcam, comboBoxDevices);
            qrCodeScanningService = new QRCodeScanningService(pictureBoxWebcam);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1795, 793);
            Controls.Add(comboBoxDevices);
            Controls.Add(buttonStartWebcam);
            Controls.Add(buttonStopWebcam);
            Controls.Add(pictureBoxWebcam);
            Controls.Add(textBoxInput);
            Controls.Add(buttonGenerate);
            Controls.Add(pictureBoxQRCode);
            Name = "MainForm";
            Text = "QR Code App";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxQRCode).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWebcam).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
