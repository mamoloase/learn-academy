using MediatR;
using FluentValidation;
using Application.Behaviours;
using Domain.Models.Responses;

namespace Application.Features.Auth.Requests;
public class CreateUserRequest : IRequest<Response>
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(50).MinimumLength(3);
            
        RuleFor(x => x.Phone).NotEmpty().IsPhone();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().IsPassword();
    }
}