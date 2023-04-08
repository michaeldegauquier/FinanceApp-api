using FluentValidation;

namespace FinanceApp.Api.Application.Handlers.AuthenticationHandlers.RegisterHandler
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Your email cannot be empty.")
                .EmailAddress().WithMessage("Your email must be valid.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.\$]+").WithMessage("Your password must contain at least one (!? *.$).");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Your first name cannot be empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Your last name cannot be empty.");
        }
    }
}
