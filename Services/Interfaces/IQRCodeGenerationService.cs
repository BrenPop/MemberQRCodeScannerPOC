namespace MemberQRCodeScannerPOC.Services.Interfaces
{
    public interface IQRCodeGenerationService
    {
        void GenerateQRCode(string text);
    }
}