using Dfo.Main.Domain.Dto;
using System.Collections.Generic;

namespace Dfo.Main.Domain.Queries
{
    public class GetUsersQuery : Query<List<UserDto>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
