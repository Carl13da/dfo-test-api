using Dfo.Main.Domain.Dto;

namespace Dfo.Main.Domain.Queries
{
    public class GetUserQuery : Query<UserDto>
    {
        public int UserId { get; set; }

        public GetUserQuery(int id)
        {
            UserId = id;
        }

        public override bool IsValid()
        {
            return UserId != 0;
        }
    }
}
