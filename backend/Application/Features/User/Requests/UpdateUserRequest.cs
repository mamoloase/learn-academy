using MediatR;
using FluentValidation;
using Domain.Models.Responses;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Requests;
public class UpdateUserRequest : IRequest<Response>
{
    public string Name { get; set; }
    public IFormFile Image { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(50).MinimumLength(3);
            
        RuleFor(x => x.Image)
            .Cascade(CascadeMode.Stop)
            .Must(x => x == null || x.Length <= 1024 * 1024 * 10)
            .WithMessage("File size is larger than allowed")
            .When(x => x.Image != null)
            .Must(x => x == null || x.ContentType.Equals("image/jpg") || x.ContentType.Equals("image/jpeg"))
            .WithMessage("File type is not allowed");
    }
}