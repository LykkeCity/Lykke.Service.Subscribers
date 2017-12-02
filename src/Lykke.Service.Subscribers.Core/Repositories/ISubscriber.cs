namespace Lykke.Service.Subscribers.Core.Repositories
{
    public interface ISubscriber
    {
        string Email { get; set; }
        string Source { get; set; }
        string PartnerId { get; set; }
    }
}
