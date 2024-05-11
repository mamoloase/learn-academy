using MediatR;
using FluentValidation;
using Domain.Models.Responses;

namespace Application.Features.Category.Requests;
public class DeleteCategoryRequest : IRequest<Response>
{
    public string Id { get; set; }

}

public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
