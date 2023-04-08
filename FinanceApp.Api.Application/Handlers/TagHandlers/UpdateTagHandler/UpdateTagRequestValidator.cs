using FluentValidation;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.UpdateTagHandler
{
    public class UpdateTagRequestValidator : AbstractValidator<UpdateTagRequest>
    {
        public UpdateTagRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag Name cannot be empty.");
        }
    }
}
