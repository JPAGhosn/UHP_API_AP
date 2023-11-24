using System.Threading;
using System.Threading.Tasks;

namespace UHP.Infrastructure.Interfaces
{
    public interface IQrCodeService
    {
        Task<string> GenerateQrCode(long id, CancellationToken cancellationToken);
    }
}