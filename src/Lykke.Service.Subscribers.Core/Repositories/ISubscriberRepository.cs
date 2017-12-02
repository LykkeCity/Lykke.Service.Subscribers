using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.Subscribers.Core.Repositories
{
    public interface ISubscriberRepository
    {
        Task<IEnumerable<ISubscriber>> GetAllAsync(string source);
        Task<ISubscriber> GetAsync(string email, string source);

        Task<ISubscriber> CreateAsync(ISubscriber subscriber);
        Task DeleteAsync(string email, string source);
    }
}
