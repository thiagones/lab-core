using FluentValidation;
using lab.domain.Models.Api;

namespace lab.domain.Validators
{
    public class UserApiModelValidator : AbstractValidator<UserApiModel>
    {

        public UserApiModelValidator()
        {
            RuleFor(user => user.Id)
                .Null();

            RuleFor(user => user.FirstName)
                .NotEmpty();

            RuleFor(user => user.LastName)
                .NotEmpty();

            RuleFor(user => user.Username)
                .MinimumLength(5)
                .WithMessage($"UserName deve possuir no mínimo 5 caracteres");

            RuleFor(user => user.Password)
                .MinimumLength(6)
                .WithMessage("Password deve possuir no mínimo 6 caracteres");

            RuleFor(user => user.Token)
                .Null();

        }
    }
}