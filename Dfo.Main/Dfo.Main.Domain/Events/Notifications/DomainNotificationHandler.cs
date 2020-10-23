using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dfo.Main.Domain.Events.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;
        private const string COMMAND_RESULT_KEY = "CommandResult";

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);

            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual void Clear()
        {
            _notifications.Clear();
        }

        public virtual bool HasNotifications() => GetNotifications().Any();

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }

        public bool HasCommandNotifications() => GetCommandNotifications().Any();

        public virtual List<DomainNotification> GetCommandNotifications() => _notifications.Where(t => t.Key == COMMAND_RESULT_KEY).ToList();
    }
}
