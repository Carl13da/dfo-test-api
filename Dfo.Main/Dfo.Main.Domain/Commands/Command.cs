using FluentValidation.Results;
using System;
using Dfo.Main.Domain.Events;

namespace Dfo.Main.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; set; }
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
