﻿using FluentValidation;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.UpdateTagHandler
{
    public class UpdateTagRequestValidator : AbstractValidator<UpdateTagRequest>
    {
        public UpdateTagRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag Name cannot be empty.")
                .MinimumLength(1).WithMessage("Tag Name should have at least 1 character.")
                .MaximumLength(20).WithMessage("Tag Name should not have more than 20 characters.");
        }
    }
}
