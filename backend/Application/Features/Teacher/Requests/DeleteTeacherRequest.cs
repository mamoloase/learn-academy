using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Teacher.Requests;
public class DeleteTeacherRequest : IRequest<Response>
{
    public string Id { get; set; }

}

public class DeleteTeacherRequestValidator : AbstractValidator<DeleteTeacherRequest>
{
    public DeleteTeacherRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
