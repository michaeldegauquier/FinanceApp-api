using FluentValidation;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.UpdateIncomeExpenseHandler
{
    public class UpdateIncomeExpenseRequestValidator : AbstractValidator<UpdateIncomeExpenseRequest>
    {
        public UpdateIncomeExpenseRequestValidator()
        {
            RuleFor(x => x.DateCreated)
                .NotEmpty().WithMessage("IncomeExpense DateCreated cannot be empty.");

            RuleFor(x => x.Notes)
                .MaximumLength(250).WithMessage("IncomeExpense Notes should not have more than 250 characters.");
        }
    }
}
