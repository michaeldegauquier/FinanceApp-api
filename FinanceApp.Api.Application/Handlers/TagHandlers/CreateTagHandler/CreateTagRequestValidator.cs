using FluentValidation;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.CreateTagHandler
{
    public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
    {
        public CreateTagRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag Name cannot be empty.");
        }
    }
}
