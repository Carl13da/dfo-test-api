using Dfo.Main.Domain.Dto;
using Dfo.Main.Domain.Validations;

namespace Dfo.Main.Domain.Commands
{
    public class UserCommand : Command
    {
        public UserDto User { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
