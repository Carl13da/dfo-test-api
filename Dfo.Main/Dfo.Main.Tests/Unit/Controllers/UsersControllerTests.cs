using System.Threading.Tasks;
using Dfo.Main.Api.Controllers;
using Dfo.Main.Domain.Queries;
using Dfo.Main.Test.Unit.Helpers;
using Xunit;

namespace Dfo.Main.Test.Unit.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task GetUserShouldCallGetUserQuery()
        {
            var mediator = new MockMediatorHandler();
            var controller = MockController(mediator);

            await controller.GetUser(1);

            Assert.True(mediator.HasNotificationWithType<GetUserQuery>());
        }

        [Fact]
        public async Task GetUsersShouldCallGetUsersQuery()
        {
            var mediator = new MockMediatorHandler();
            var controller = MockController(mediator);

            await controller.GetUsers();

            Assert.True(mediator.HasNotificationWithType<GetUsersQuery>());
        }

        private UsersController MockController(MockMediatorHandler mediator = null)
        {
            return new UsersController(mediator: mediator ?? new MockMediatorHandler());
        }
    }
}
