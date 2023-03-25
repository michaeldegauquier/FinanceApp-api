﻿using FluentValidation;

namespace FinanceApp.Api.Application.Handlers.Authentication.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Your email cannot be empty.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Your password cannot be empty.");
        }
    }
}
