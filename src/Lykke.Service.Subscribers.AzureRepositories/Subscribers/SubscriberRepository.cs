using AzureStorage;
using Lykke.Service.Subscribers.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.Subscribers.AzureRepositories.Subscribers
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly INoSQLTableStorage<Subscriber> _tableStorage;

        public SubscriberRepository(INoSQLTableStorage<Subscriber> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<ISubscriber> GetAsync(string email, string source)
        {
            var partitionKey = Subscriber.GeneratePartitionKey(source);
            var rowKey = Subscriber.GenerateRowKey(email);

            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }

        public async Task<IEnumerable<ISubscriber>> GetAllAsync(string source)
        {
            var partitionKey = Subscriber.GeneratePartitionKey(source);
            return await _tableStorage.GetDataAsync(partitionKey);
        }

        public async Task<ISubscriber> CreateAsync(ISubscriber subscriber)
        {
            var partitionKey = Subscriber.GeneratePartitionKey(subscriber.Source);
            var rowKey = Subscriber.GenerateRowKey(subscriber.Email);

            var entity = await _tableStorage.GetDataAsync(partitionKey, rowKey);

            if (entity != null) throw new Exception("Email exists: " + subscriber.Email);

            var newEntity = Subscriber.Create(subscriber);
            await _tableStorage.InsertAsync(newEntity);

            return newEntity;
        }

        public async Task DeleteAsync(string email, string source)
        {
            var partitionKey = Subscriber.GeneratePartitionKey(source);
            var rowKey = Subscriber.GenerateRowKey(email); ;

            var entity = await _tableStorage.GetDataAsync(partitionKey, rowKey);
            await _tableStorage.DeleteAsync(entity.PartitionKey, entity.RowKey);
        }
    }

}
