using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using QRCoder;
using UHP.Infrastructure.Interfaces;

namespace UHP.Infrastructure.Service
{
    public class QrCodeService : IQrCodeService
    {
        public async Task<string> GenerateQrCode(long id, CancellationToken cancellationToken)
        {
            PayloadGenerator.Url generator =
                new PayloadGenerator.Url($"http://192.168.16.6:4200/pharmacist/prescription/{id}");
            string payload = generator.ToString();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            Guid qrCodeGuid = Guid.NewGuid();
            qrCodeImage.Save($"wwwroot/prescription/{qrCodeGuid}.png");
            return $"prescription/{qrCodeGuid}.png";
        }
    }
}