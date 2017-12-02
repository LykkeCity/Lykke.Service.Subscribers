using Lykke.Service.Subscribers.Client.AutorestClient.Models;
using Lykke.Service.Subscribers.Client.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.Subscribers.Client
{
    public interface ISubscribersClient
    {
        Task<IEnumerable<SubscriberResponse>> GetAsync(string source);
        Task<SubscriberResponse> GetByEmailAsync(string source, string email);
        Task CreateSubscriberAsync(SubscriberRequestModel model);
        Task DeleteSubscriberAsync(string source, string email);
    }
}
