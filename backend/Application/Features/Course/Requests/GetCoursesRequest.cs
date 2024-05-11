using MediatR;
using Domain.Models.Responses;
using Domain.Models.Requests;

namespace Application.Features.Course.Requests;
public class GetCoursesRequest : RequestPagination, IRequest<Response>
{
    public string CategoryId { get; set; }
    public string TetacherId { get; set; }
    public CourseOrderBy OrderBy { get; set; }
}
public enum CourseOrderBy
{
    Date = 0,
    DateDescending = 1,
    Price = 2,
    PriceDescending = 3,
}