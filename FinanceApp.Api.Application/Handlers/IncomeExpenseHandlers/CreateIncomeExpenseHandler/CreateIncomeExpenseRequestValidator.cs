using FluentValidation;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.CreateIncomeExpenseHandler
{
    public class CreateIncomeExpenseRequestValidator : AbstractValidator<CreateIncomeExpenseRequest>
    {
        public CreateIncomeExpenseRequestValidator()
        {
            RuleFor(x => x.DateCreated)
                .NotEmpty().WithMessage("IncomeExpense DateCreated cannot be empty.");

            RuleFor(x => x.Notes)
                .MaximumLength(250).WithMessage("IncomeExpense Notes should not have more than 250 characters.");
        }
    }
}
