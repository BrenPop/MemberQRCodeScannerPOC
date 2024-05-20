using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberQRCodeScannerPOC.Services.Interfaces;

namespace MemberQRCodeScannerPOC.Services
{
    public class WebcamService : IWebcamService
    {
        private PictureBox _pictureBoxWebcam;
        private ComboBox _comboBoxDevices;
        private FilterInfoCollection _videoDevices;
        private VideoCaptureDevice _videoSource;

        public WebcamService(PictureBox pictureBoxWebcam, ComboBox comboBoxDevices)
        {
            _pictureBoxWebcam = pictureBoxWebcam;
            _comboBoxDevices = comboBoxDevices;
        }

        public void LoadDevices()
        {
            _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in _videoDevices)
            {
                _comboBoxDevices.Items.Add(device.Name);
            }
            _comboBoxDevices.SelectedIndex = 0;
        }

        public void StartWebcam()
        {
            _videoSource = new VideoCaptureDevice(_videoDevices[_comboBoxDevices.SelectedIndex].MonikerString);
            _videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            _videoSource.Start();
        }

        public void StopWebcam()
        {
            _videoSource.SignalToStop();
            _videoSource.WaitForStop();
        }

        public void Cleanup()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.WaitForStop();
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            _pictureBoxWebcam.Image = bitmap;
        }
    }

}
