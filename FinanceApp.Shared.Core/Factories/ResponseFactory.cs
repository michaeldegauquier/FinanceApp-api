using FinanceApp.Shared.Core.Enums;
using FinanceApp.Shared.Core.Helpers.EnumHelpers;
using FinanceApp.Shared.Core.Responses;

namespace FinanceApp.Shared.Core.Factories
{
    public static class ResponseFactory
    {
        public static DataResponse<T> Create<T>(T data)
        {
            return CreateDataResponse(data);
        }

        public static DataResponse<T> Error<T>(ErrorType errorType)
        {
            return CreateErrorResponse<T>(errorType);
        }

        private static DataResponse<T> CreateDataResponse<T>(T data)
        {
            return new DataResponse<T>
            {
                Data = data,
                Error = new Error
                {
                    ErrorType = ErrorType.None,
                    Value = ErrorType.None.ToString(),
                    Message = EnumReader.GetDescription(ErrorType.None)
                }
            };
        }

        private static DataResponse<T> CreateErrorResponse<T>(ErrorType errorType)
        {
            return new DataResponse<T>
            {
                Error = new Error
                {
                    ErrorType = errorType,
                    Value = errorType.ToString(),
                    Message = EnumReader.GetDescription(errorType)
                }
            };
        }
    }
}
