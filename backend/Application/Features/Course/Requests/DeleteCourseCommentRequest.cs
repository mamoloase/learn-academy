using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Course.Requests;
public class DeleteCourseCommentRequest : IRequest<Response>
{
    public string Id { get; set; }

}

public class DeleteCourseCommentRequestValidator : AbstractValidator<DeleteCourseCommentRequest>
{
    public DeleteCourseCommentRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}