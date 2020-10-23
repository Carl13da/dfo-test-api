using Dfo.Main.Domain.Interfaces.Events;
using MediatR;
using System;

namespace Dfo.Main.Domain.Queries
{
    public class QueryMessage<TResponse> : IRequest<TResponse>, IBaseRequest, IRequestBase
    {
        protected QueryMessage()
        {
            MessageType = GetType().Name;
        }

        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
}
