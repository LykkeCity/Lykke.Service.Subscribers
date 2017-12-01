using System.Threading.Tasks;

namespace Lykke.Service.Subscribers.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}