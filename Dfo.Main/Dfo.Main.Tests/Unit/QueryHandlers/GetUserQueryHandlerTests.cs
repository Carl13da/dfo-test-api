using Moq;
using System;
using System.Threading.Tasks;
using Dfo.Main.Domain.Dto;
using Dfo.Main.Domain.Interfaces.Services;
using Dfo.Main.Domain.Queries;
using Dfo.Main.Domain.QueryHandlers;
using Dfo.Main.Test.Unit.Helpers;
using Xunit;

namespace Dfo.Main.Test.Unit.QueryHandlers
{
    public class GetUserQueryHandlerTests
    {
        [Fact(DisplayName = "Should isValid equals false when id equals zero")]
        public void ShouldIsValidEqualsFalseWhenIdZero()
        {
            var request = new GetUserQuery(0);

            Assert.False(request.IsValid());
        }

        [Fact(DisplayName = "Should call user service")]
        public async Task ShouldCallUserService()
        {
            var mediator = new MockMediatorHandler();
            var userService = new Mock<IUserService>();

            userService
                .Setup(t => t.GetUser(It.IsAny<int>()))
                .Returns(Task.FromResult(Tuple.Create(false, new UserDto(), "")));

            var handler = MockHandler(mediator, userService.Object);

            var response = await handler.AfterValidation(new GetUserQuery(1));

            Assert.False(mediator.HasNotification());
        }

        private GetUserQueryHandler MockHandler(MockMediatorHandler mediator = null, IUserService userService = null)
        {
            return new GetUserQueryHandler(mediator ?? new MockMediatorHandler(), userService ?? new Mock<IUserService>().Object);
        }
    }
}

