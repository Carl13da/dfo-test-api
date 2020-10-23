using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfo.Main.Domain.Dto;
using Dfo.Main.Domain.Interfaces.Events;
using Dfo.Main.Domain.Interfaces.Services;
using Dfo.Main.Domain.Queries;

namespace Dfo.Main.Domain.QueryHandlers
{
    public class GetUsersQueryHandler : MediatorQueryHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUsersQueryHandler(IMediatorHandler mediator, IUserService userService) : base(mediator)
        {
            _userService = userService;
        }

        public override async Task<List<UserDto>> AfterValidation(GetUsersQuery request)
        {
            var (hasError, users, error) = await _userService.GetUsers(request.Name);

            if (hasError)
            {
                NotifyError(error);

                return null;
            }

            return users;
        }
    }
}
