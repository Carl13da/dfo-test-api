using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Dfo.Main.Api.Models;
using Dfo.Main.Domain.Events.Notifications;
using Dfo.Main.Domain.Interfaces.Events;

namespace Dfo.Main.Api.Abstractions
{
    public abstract class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        protected IMediatorHandler Mediator { get; }

        public ApiController(IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)mediator.GetNotificationHandler();
            Mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (_notifications.HasNotifications())
            {
                if (result == null && _notifications.HasCommandNotifications())
                {
                    return Ok(new SuccessResponseDto<object>(_notifications
                        .GetCommandNotifications()
                        .FirstOrDefault()
                        .Value)
                    );
                }

                if (result == null)
                {
                    return BadRequest(new ErrorResponseDto(Notifications.FirstOrDefault().Value.ToString()));
                }
                
            }

            return Ok(new SuccessResponseDto<object>(result));
        }
    }

    
}