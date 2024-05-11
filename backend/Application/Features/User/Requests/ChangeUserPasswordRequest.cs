
using System.Security.Claims;
using Application.Behaviours;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Requests;
public class ChangeUserPasswordRequest : IRequest<Response>
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public class ChangeUserPasswordRequestValidator : AbstractValidator<ChangeUserPasswordRequest>
{
    public ChangeUserPasswordRequestValidator()
    {
        RuleFor(x => x.Password).NotNull().IsPassword();
        RuleFor(x => x.NewPassword).NotNull().IsPassword();
    }
}