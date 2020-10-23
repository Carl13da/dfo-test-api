using Microsoft.Extensions.DependencyInjection;
using Dfo.Main.Infrastructure.Contexts;

namespace Dfo.Main.IoC.Initializers
{
    public class InitializeMockDBContext
    {
        public static void InitializeMockDBContexts(IServiceCollection services)
        {
            MockContext.ConfigureMockContext(services);
        }
    }
}
