using Autofac;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.Subscribers.AzureRepositories.Subscribers;
using Lykke.Service.Subscribers.Core.Repositories;
using Lykke.Service.Subscribers.Core.Services;
using Lykke.Service.Subscribers.Core.Settings.ServiceSettings;
using Lykke.Service.Subscribers.Services;
using Lykke.SettingsReader;

namespace Lykke.Service.Subscribers.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<SubscribersSettings> _settings;
        private readonly ILog _log;

        public ServiceModule(IReloadingManager<SubscribersSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance<ISubscriberRepository>(
                  new SubscriberRepository(
                       AzureTableStorage<Subscriber>.Create(_settings.ConnectionString(x => x.Db.MainConnectionString), "Subscribers",
                          _log)));

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();
        }
    }
}
