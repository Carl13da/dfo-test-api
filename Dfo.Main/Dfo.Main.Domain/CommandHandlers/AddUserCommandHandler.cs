using System;
using System.Threading.Tasks;
using Dfo.Main.Domain.Commands;
using Dfo.Main.Domain.Interfaces.Events;
using Dfo.Main.Domain.Interfaces.Services;

namespace Dfo.Main.Domain.CommandHandlers
{
    public class AddUserCommandHandler : MediatorCommandHandler<AddUserCommand>
    {
        private readonly IUserService _userService;

        public AddUserCommandHandler(IMediatorHandler mediator, IUserService userService) : base(mediator)
        {
            _userService = userService;
        }

        public override async Task AfterValidation(AddUserCommand request)
        {
            var (hasError, error) = await _userService.AddUser(request.User);

            if (hasError)
            {
                NotifyError(error);

                return;
            }
        }
    }
}
