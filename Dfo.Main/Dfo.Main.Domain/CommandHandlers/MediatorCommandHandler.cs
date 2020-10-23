using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dfo.Main.Domain.Commands;
using Dfo.Main.Domain.Events.Notifications;
using Dfo.Main.Domain.Interfaces.Events;

namespace Dfo.Main.Domain.CommandHandlers
{
    public abstract class MediatorCommandHandler<TCommand> : AsyncRequestHandler<TCommand>
        where TCommand : Command
    {
        protected IMediatorHandler Mediator { get; }

        protected MediatorCommandHandler(IMediatorHandler mediator)
        {
            Mediator = mediator;
        }

        public abstract Task AfterValidation(TCommand request);

        protected override Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);

                return Task.CompletedTask;
            }

            return AfterValidation(request);
        }

        private void NotifyValidationErrors(TCommand request)
        {
            foreach (var error in request.ValidationResult.Errors)
            {
                Mediator.RaiseEvent(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
        }

        protected bool HasNotifications() => Mediator.HasNotification();
        protected void NotifyError(string message)
        {
            Mediator.RaiseEvent(new DomainNotification(string.Empty, message));
        }

        protected void NotifyCommandResult(object result) => Mediator.RaiseEvent(new DomainNotification("CommandResult", result));
    }
}
