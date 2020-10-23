using System;
using System.Threading.Tasks;
using Dfo.Main.Domain.Commands;
using Dfo.Main.Domain.Interfaces.Events;
using Dfo.Main.Domain.Interfaces.Services;

namespace Dfo.Main.Domain.CommandHandlers
{
    public class UpdateUserCommandHandler : MediatorCommandHandler<UpdateUserCommand>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IMediatorHandler mediator, IUserService userService) : base(mediator)
        {
            _userService = userService;
        }

        public override async Task AfterValidation(UpdateUserCommand request)
        {
            var (hasError, error) = await _userService.UpdateUser(request.User);

            if (hasError)
            {
                NotifyError(error);

                return;
            }
        }
    }
}
