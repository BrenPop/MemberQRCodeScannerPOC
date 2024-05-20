using MemberQRCodeScannerPOC.Services.Interfaces;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberQRCodeScannerPOC.Services
{
    public class QRCodeGenerationService : IQRCodeGenerationService
    {
        private PictureBox _pictureBoxQRCode;

        public QRCodeGenerationService(PictureBox pictureBoxQRCode)
        {
            _pictureBoxQRCode = pictureBoxQRCode;
        }

        public void GenerateQRCode(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            _pictureBoxQRCode.Image = qrCodeImage;

            SaveQRCodeImage(qrCodeImage, text);
        }

        private void SaveQRCodeImage(Bitmap qrCodeImage, string text)
        {
            string projectRootPath = Directory.GetCurrentDirectory();
            string fileName = text + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "-QRCode.png";
            string filePath = Path.Combine(projectRootPath, fileName);

            qrCodeImage.Save(filePath, ImageFormat.Png);
        }
    }

}
