using MediatR;
using FluentValidation;

using Application.Behaviours;
using Domain.Models.Responses;

namespace Application.Features.Auth.Requests;
public class SignInRequest : IRequest<Response>
{
    public string Email { get; set; }
    public string Password { get; set; }
} 

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().IsPassword();
    }
}