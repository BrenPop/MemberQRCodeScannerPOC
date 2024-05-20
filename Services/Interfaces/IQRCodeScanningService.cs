namespace MemberQRCodeScannerPOC.Services.Interfaces
{
    public interface IQRCodeScanningService
    {
        void StartScanning();
        void StopScanning();
    }
}