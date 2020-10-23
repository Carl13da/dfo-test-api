using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dfo.Main.Domain.CommandHandlers;
using Dfo.Main.Domain.Commands;
using Dfo.Main.Domain.Dto;
using Dfo.Main.Domain.Events.Bus;
using Dfo.Main.Domain.Events.Notifications;
using Dfo.Main.Domain.Interfaces.Events;
using Dfo.Main.Domain.Interfaces.Repositories;
using Dfo.Main.Domain.Interfaces.Services;
using Dfo.Main.Domain.Queries;
using Dfo.Main.Domain.QueryHandlers;
using Dfo.Main.Domain.Services;
using Dfo.Main.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Dfo.Main.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // ASP.NET HttpContenxt dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // DOmain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain Service
            services.AddScoped<IUserService, UserService>();

            // Repos
            services.AddScoped<IUserRepository, UserRepository>();

            // Domain Queries or Commands
            services.AddScoped<IRequestHandler<GetUserQuery, UserDto>, GetUserQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsersQuery, List<UserDto>>, GetUsersQueryHandler>();

            services.AddScoped<IRequestHandler<AddUserCommand, Unit>, AddUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, Unit>, UpdateUserCommandHandler>();
        }
    }
}
