using System.Security.Claims;
using Domain.Models.Requests;
using Domain.Models.Responses;
using MediatR;

namespace Application.Features.Order.Requests;
public class GetOrdersRequest : RequestPagination, IRequest<Response>
{
    public ClaimsPrincipal User { get; set; }
}
