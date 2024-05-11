using MediatR;
using Domain.Models.Requests;
using Domain.Models.Responses;

namespace Application.Features.Teacher.Requests;
public class GetTeachersRequest : RequestPagination, IRequest<Response>
{

}
