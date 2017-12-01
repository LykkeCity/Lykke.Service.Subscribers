using Lykke.Service.Subscribers.Client.AutorestClient.Models;

namespace Lykke.Service.Subscribers.Client.Models.ResponseModels
{
    public class SubscriberResponse
    {
        public string Email { get; set; }
        public string Source { get; set; }
        public string PartnerId { get; set; }

        public static SubscriberResponse Create(SubscriberResponseModel model)
        {
            return new SubscriberResponse
            {
                Email = model.Email,
                Source = model.Source,
                PartnerId = model.PartnerId
            };
        }
    }
}
