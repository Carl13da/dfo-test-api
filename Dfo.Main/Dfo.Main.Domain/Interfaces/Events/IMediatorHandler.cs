using MediatR;
using System.Threading.Tasks;
using Dfo.Main.Domain.Commands;
using Dfo.Main.Domain.Events;
using Dfo.Main.Domain.Events.Notifications;
using Dfo.Main.Domain.Queries;

namespace Dfo.Main.Domain.Interfaces.Events
{
    public interface IMediatorHandler
    {
        void ClearNotifications();
        bool HasNotification();
        Task RaiseEvent<T>(T @event) where T : Event;
        Task<TResponse> SendQuery<TResponse>(Query<TResponse> query) where TResponse : class;
        Task SendCommand<T>(T command) where T : Command;
        INotificationHandler<DomainNotification> GetNotificationHandler();
    }
}
