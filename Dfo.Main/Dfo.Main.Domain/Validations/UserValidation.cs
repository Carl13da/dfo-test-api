using FluentValidation;
using Dfo.Main.Domain.Commands;

namespace Dfo.Main.Domain.Validations
{
    public class UserValidation : AbstractValidator<UserCommand>
    {
        public UserValidation()
        {
            RuleFor(x => x.User)
                .NotNull()
                .WithMessage("Entry payload cannot be null");

            RuleFor(x => x.User.Name)
                .NotNull()
                    .WithMessage("User Name cannot be null or should")
                .MaximumLength(50)
                    .WithMessage("Max length for User Name is 50!");

            RuleFor(x => x.User.Age)
                .NotNull()
                .WithMessage("User Age cannot be null");

            RuleFor(x => x.User.Address)
                .NotNull()
                    .WithMessage("User Address cannot be null or should")
                .MaximumLength(50)
                    .WithMessage("Max length for User Address is 50!");
        }
        
    }
}
