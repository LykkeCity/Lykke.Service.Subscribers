using Common.Log;
using Lykke.Service.Subscribers.Client.AutorestClient;
using Lykke.Service.Subscribers.Client.AutorestClient.Models;
using Lykke.Service.Subscribers.Client.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.Service.Subscribers.Client
{
    public class SubscribersClient : ISubscribersClient, IDisposable
    {
        private readonly ILog _log;
        private SubscribersAPI _apiClient;

        public SubscribersClient(string serviceUrl, ILog log, int timeout)
        {
            _log = log;
            _apiClient =
              new SubscribersAPI(new Uri(serviceUrl))
              {
                  HttpClient = { Timeout = TimeSpan.FromSeconds(timeout) }
              };
        }

        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }

        public async Task<IEnumerable<SubscriberResponse>> GetAsync(string source)
        {
            var response = await _apiClient.GetSubsribersAsync(source);

            return response == null
                            ? new SubscriberResponse[0]
                            : response.Select(x => SubscriberResponse.Create(x));
        }

        public async Task<SubscriberResponse> GetByEmailAsync(string source, string email)
        {
            var response = await _apiClient.GetSubsribersByEmailAsync(source: source, email: email);

            return CreateNullableModel(response);
        }

        public async Task CreateSubscriberAsync(SubscriberRequestModel model)
        {
            await _apiClient.CreateAsyncAsync(model);
        }

        public async Task DeleteSubscriberAsync(string source, string email)
        {
            await _apiClient.DeleteAsyncAsync(email: email, source: source);
        }

        private SubscriberResponse CreateNullableModel(SubscriberResponseModel source)
        {
            return source != null ? SubscriberResponse.Create(source) : null;
        }
    }
}
