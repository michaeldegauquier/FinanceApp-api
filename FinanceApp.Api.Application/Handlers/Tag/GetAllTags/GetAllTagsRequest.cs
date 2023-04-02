using FinanceApp.Shared.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Api.Application.Handlers.Tag.GetAllTags
{
    public class GetAllTagsRequest : IRequest<DataResponse<GetAllTagsResponse>>
    {
    }
}
