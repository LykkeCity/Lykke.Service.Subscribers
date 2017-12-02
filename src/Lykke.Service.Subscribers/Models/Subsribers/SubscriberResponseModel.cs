using Lykke.Service.Subscribers.Core.Repositories;

namespace Lykke.Service.Subscribers.Models.Subsribers
{
    public class SubscriberResponseModel
    {
        public string Email { get; set; }
        public string Source { get; set; }
        public string PartnerId { get; set; }

        public static SubscriberResponseModel Create(ISubscriber subscriber)
        {
            return new SubscriberResponseModel()
            {
                Email = subscriber.Email,
                PartnerId = subscriber.PartnerId,
                Source = subscriber.Source
            };
        }
    }
}
