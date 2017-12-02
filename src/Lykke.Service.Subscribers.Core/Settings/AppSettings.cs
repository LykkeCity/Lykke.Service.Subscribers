using Lykke.Service.Subscribers.Core.Settings.ServiceSettings;
using Lykke.Service.Subscribers.Core.Settings.SlackNotifications;

namespace Lykke.Service.Subscribers.Core.Settings
{
    public class AppSettings
    {
        public SubscribersSettings SubscribersService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
