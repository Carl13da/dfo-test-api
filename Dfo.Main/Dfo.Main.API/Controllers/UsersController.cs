using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dfo.Main.Api.Abstractions;
using Dfo.Main.Domain.Commands;
using Dfo.Main.Domain.Interfaces.Events;
using Dfo.Main.Domain.Queries;
using Dfo.Main.Domain.Dto;

namespace Dfo.Main.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ApiController
    {
        public UsersController(IMediatorHandler mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("byId")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            return Response(await Mediator.SendQuery(new GetUserQuery(id)));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetUsers()
        {
            return Response(await Mediator.SendQuery(new GetUsersQuery()));
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
        {
            await Mediator.SendCommand(new AddUserCommand { User = dto });

            return Response();
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto dto)
        {
            await Mediator.SendCommand(new UpdateUserCommand { User = dto });

            return Response();
        }
    }
}