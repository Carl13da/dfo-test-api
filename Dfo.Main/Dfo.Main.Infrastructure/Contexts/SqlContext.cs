using Microsoft.EntityFrameworkCore;
using Dfo.Main.Infrastructure.Mappings;
using Dfo.Main.Domain.Models;

namespace Dfo.Main.Infrastructure.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserMap.Map(modelBuilder);
        }
    }
}
