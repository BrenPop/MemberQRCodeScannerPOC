namespace MemberQRCodeScannerPOC.Services.Interfaces
{
    public interface IWebcamService
    {
        void Cleanup();
        void LoadDevices();
        void StartWebcam();
        void StopWebcam();
    }
}