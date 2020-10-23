using Dfo.Main.Domain.Models;
using Dfo.Main.Infrastructure.Contexts;

namespace Dfo.Main.Infrastructure.Data
{
    public class MockData
    {
        public static User User1 { get; } =
            new User
            {
                Id = 1,
                Name = "Carlos",
                Age = 26,
                Address = "Rua Kobe"
            };

        public static User User2 { get; } =
            new User
            {
                Id = 2,
                Name = "Cintia",
                Age = 35,
                Address = "Rua Kobe"
            };

        public static User User3 { get; } =
            new User
            {
                Id = 3,
                Name = "Luna",
                Age = 6,
                Address = "Rua Kobe"
            };

        public static User User4 { get; } =
            new User
            {
                Id = 4,
                Name = "Sofia",
                Age = 3,
                Address = "Rua Kobe"
            };

        public static void SeedTestData(SqlContext context)
        {
            context.Users.AddRange(User1, User2, User3, User4);

            context.SaveChanges();
        }
    }
}
