using Domain.Models.Requests;
using Domain.Models.Responses;
using MediatR;

namespace Application.Features.User.Requests;
public class GetUsersRequest : RequestPagination, IRequest<Response>
{

}
