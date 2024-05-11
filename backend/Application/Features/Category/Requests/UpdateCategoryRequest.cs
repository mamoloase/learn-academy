using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Category.Requests;
public class UpdateCategoryRequest : IRequest<Response>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string ParentId { get; set; }
}
public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(20).MinimumLength(3);

        RuleFor(x => x.Description).NotEmpty()
            .MaximumLength(500);
    }
}
