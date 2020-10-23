using MediatR;
using System;
using Dfo.Main.Domain.Interfaces.Events;

namespace Dfo.Main.Domain.Events
{
    public abstract class Message : IRequest, IRequestBase
    {
        protected Message()
        {
            MessageType = GetType().Name;
        }

        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
}
