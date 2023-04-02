using FinanceApp.Shared.Core.Responses;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.IncomeExpense.GetAllIncomesExpenses
{
    public class GetAllIncomesExpensesRequest : IRequest<DataResponse<GetAllIncomesExpensesResponse>>
    {
    }
}
