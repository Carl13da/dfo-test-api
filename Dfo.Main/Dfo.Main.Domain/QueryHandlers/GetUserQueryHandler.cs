using System;
using System.Threading.Tasks;
using Dfo.Main.Domain.Dto;
using Dfo.Main.Domain.Interfaces.Events;
using Dfo.Main.Domain.Interfaces.Services;
using Dfo.Main.Domain.Queries;

namespace Dfo.Main.Domain.QueryHandlers
{
    public class GetUserQueryHandler : MediatorQueryHandler<GetUserQuery, UserDto>
    {
        private readonly IUserService _userService;

        public GetUserQueryHandler(IMediatorHandler mediator, IUserService userService) : base(mediator)
        {
            _userService = userService;
        }

        public override async Task<UserDto> AfterValidation(GetUserQuery request)
        {
            var (hasError, user, error) = await _userService.GetUser(request.UserId);

            if (hasError)
            {
                NotifyError(error);

                return null;
            }

            return user;
        }
    }
}
