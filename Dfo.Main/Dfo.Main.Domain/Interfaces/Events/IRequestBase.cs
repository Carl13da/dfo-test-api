using System;

namespace Dfo.Main.Domain.Interfaces.Events
{
    public interface IRequestBase
    {
        string MessageType { get; }
        Guid AggregateId { get; }
    }
}
