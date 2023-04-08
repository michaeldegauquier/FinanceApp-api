using FinanceApp.Shared.Core.Responses;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetAllIncomesExpensesHandler
{
    public class GetAllIncomesExpensesRequest : IRequest<DataResponse<GetAllIncomesExpensesResponse>>
    {
    }
}
