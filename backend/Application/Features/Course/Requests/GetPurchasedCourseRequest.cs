using System.Security.Claims;
using Domain.Models.Requests;
using Domain.Models.Responses;
using MediatR;

namespace Application.Features.Course.Requests;
public class GetPurchasedCourseRequest : RequestPagination ,IRequest<Response>
{
    public ClaimsPrincipal User { get; set; }
}
