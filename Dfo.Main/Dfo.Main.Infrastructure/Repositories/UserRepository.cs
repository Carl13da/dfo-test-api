using Dfo.Main.Domain.Interfaces.Repositories;
using Dfo.Main.Infrastructure.Contexts;
using Dfo.Main.Domain.Models;

namespace Dfo.Main.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SqlContext sqlContext) : base(sqlContext)
        {
        }
    }
}
