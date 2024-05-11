using MediatR;
using Domain.Models.Responses;
using FluentValidation;

namespace Application.Features.Course.Requests;
public class GetCourseRequest : IRequest<Response>
{
    public string Id { get; set; }

}
public class GetCourseRequestValidator : AbstractValidator<GetCourseRequest>
{
    public GetCourseRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}