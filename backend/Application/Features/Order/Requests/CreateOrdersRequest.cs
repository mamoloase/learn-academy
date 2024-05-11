using System.Security.Claims;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Requests;
public class CreateOrdersRequest : IRequest<Response>
{
    public string CourseId { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public class CreateOrdersRequestValidator : AbstractValidator<CreateOrdersRequest>
{
    public CreateOrdersRequestValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();

    }
}