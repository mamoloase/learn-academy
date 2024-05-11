using System.Security.Claims;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Course.Requests;
public class CreateCourseCommentRequest : IRequest<Response>
{
    public string Message { get; set; }

    public string AnswerId { get; set; }
    public string CourseId { get; set; }

    public ClaimsPrincipal User { get; set; }
}
public class CreateCourseCommentRequestValidator : AbstractValidator<CreateCourseCommentRequest>
{
    public CreateCourseCommentRequestValidator()
    {
        RuleFor(x => x.Message).NotEmpty()
            .MaximumLength(150).MinimumLength(3);

        RuleFor(x => x.CourseId).NotEmpty();
    }
}