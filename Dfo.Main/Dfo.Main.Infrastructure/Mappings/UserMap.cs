using Microsoft.EntityFrameworkCore;
using Dfo.Main.Domain.Models;

namespace Dfo.Main.Infrastructure.Mappings
{
    public class UserMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(v =>
            {
                v.HasKey(x => x.Id);

                v.Property(p => p.Name);
                v.Property(p => p.Age);
                v.Property(p => p.Address);
            });
        }
    }
}
