using Dfo.Main.Domain.Events.Notifications;
using Dfo.Main.Domain.Interfaces.Events;

namespace Dfo.Main.Domain.Services
{
    public abstract class ServiceMediator
    {
        protected IMediatorHandler Mediator { get; }

        protected ServiceMediator(IMediatorHandler mediator)
        {
            Mediator = mediator;
        }

        protected void NotifyError(string code, string message)
        {
            Mediator.RaiseEvent(new DomainNotification(code, message));
        }
        protected void NotifyError(string message) => NotifyError(message);
        protected bool HasNotification() => Mediator.HasNotification();
    }
}