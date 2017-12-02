using Lykke.Service.Subscribers.Core.Repositories;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.Subscribers.AzureRepositories.Subscribers
{
    public class Subscriber : TableEntity, ISubscriber
    {
        public static string GeneratePartitionKey(string source)
        {
            return source;
        }

        public static string GenerateRowKey(string email)
        {
            return email;
        }

        public static Subscriber Create(ISubscriber subscriber)
        {
            var result = new Subscriber
            {
                PartitionKey = GeneratePartitionKey(subscriber.Source),
                RowKey = GenerateRowKey(subscriber.Email),
                PartnerId = subscriber.PartnerId,
                Source = subscriber.Source,
                Email = subscriber.Email
            };

            return result;
        }

        public static Subscriber Get(ISubscriber subscriber)
        {
            var result = new Subscriber
            {
                PartitionKey = GeneratePartitionKey(subscriber.Source),
                RowKey = subscriber.Email,
                Email = subscriber.Email,
                Source = subscriber.Source,
                PartnerId = subscriber.PartnerId
            };

            return result;
        }

        public string Email { get; set; }
        public string Source { get; set; }
        public string PartnerId { get; set; }
    }
}
