using MediatR;
using FluentValidation;
using Domain.Models.Responses;
using System.Security.Claims;

namespace Application.Features.Course.Requests;
public class DeleteCourseRequest : IRequest<Response>
{
    public string Id { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public class DeleteCourseRequestValidator : AbstractValidator<DeleteCourseRequest>
{
    public DeleteCourseRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}