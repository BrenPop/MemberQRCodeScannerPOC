using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberQRCodeScannerPOC.Services.Interfaces;
using ZXing;
using ZXing.Windows.Compatibility;

namespace MemberQRCodeScannerPOC.Services
{
    public class QRCodeScanningService : IQRCodeScanningService
    {
        private PictureBox _pictureBoxWebcam;
        private System.Windows.Forms.Timer _qrCodeScanTimer;

        public QRCodeScanningService(PictureBox pictureBoxWebcam)
        {
            _pictureBoxWebcam = pictureBoxWebcam;
            InitializeQrCodeTimer();
        }

        private void InitializeQrCodeTimer()
        {
            _qrCodeScanTimer = new System.Windows.Forms.Timer();
            _qrCodeScanTimer.Interval = 1000;
            _qrCodeScanTimer.Tick += new EventHandler(detectQrCode_Tick);
        }

        public void StartScanning()
        {
            _qrCodeScanTimer.Start();
        }

        public void StopScanning()
        {
            _qrCodeScanTimer.Stop();
        }

        private void detectQrCode_Tick(object sender, EventArgs e)
        {
            if (_pictureBoxWebcam.Image != null)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(_pictureBoxWebcam.Image);
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
                        _qrCodeScanTimer.Stop();
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
    }

}
