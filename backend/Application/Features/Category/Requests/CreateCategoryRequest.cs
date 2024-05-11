using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Category.Requests;
public class CreateCategoryRequest : IRequest<Response>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string ParentId { get; set; }
}

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(20).MinimumLength(3);

        RuleFor(x => x.Description).NotEmpty()
            .MaximumLength(500);
    }
}
