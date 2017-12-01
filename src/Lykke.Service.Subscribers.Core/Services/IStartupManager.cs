using System.Threading.Tasks;

namespace Lykke.Service.Subscribers.Core.Services
{
    public interface IStartupManager
    {
        Task StartAsync();
    }
}