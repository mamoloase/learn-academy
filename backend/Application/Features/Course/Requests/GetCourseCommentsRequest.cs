using Domain.Models.Requests;
using Domain.Models.Responses;

using MediatR;
using FluentValidation;

namespace Application.Features.Course.Requests;
public class GetCourseCommentsRequest : RequestPagination, IRequest<Response>
{
    public string Id { get; set; }
}
public class GetCourseCommentsRequestValidator : AbstractValidator<GetCourseCommentsRequest>
{
    public GetCourseCommentsRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}