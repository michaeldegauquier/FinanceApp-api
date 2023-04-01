using FinanceApp.Shared.Core.Extensions;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;

namespace FinanceApp.Shared.Core.Factories
{
    public static class ResponseFactory
    {
        public static DataResponse<T> Success<T>(T data, SuccessType successType)
        {
            return CreateSuccessResponse(data, successType);
        }

        public static DataResponse<T> Error<T>(ErrorType errorType)
        {
            return CreateErrorResponse<T>(errorType);
        }

        private static DataResponse<T> CreateSuccessResponse<T>(T data, SuccessType successType)
        { 
            return new DataResponse<T>
            {
                Data = data,
                Status = successType.GetStatusCode(),
                Message = successType.GetMessage(),
            };
        }

        private static DataResponse<T> CreateErrorResponse<T>(ErrorType errorType)
        {
            return new DataResponse<T>
            {
                Status = errorType.GetStatusCode(),
                Message = errorType.GetMessage(),
                Error = new Error
                {
                    ErrorType = errorType,
                    Code = errorType.ToString(),
                }
            };
        }
    }
}
