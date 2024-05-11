using MediatR;
using FluentValidation;
using Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Teacher.Requests;
public class UpdateTeacherRequest : IRequest<Response>
{
    public string Id { get; set; }
    public string Description { get; set; }

    public IFormFile Image { get; set; }
    
    public ClaimsPrincipal User { get; set; }

}
public class UpdateTeacherRequestValidator : AbstractValidator<UpdateTeacherRequest>
{
    public UpdateTeacherRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);

        RuleFor(x => x.Image)
            .Cascade(CascadeMode.Stop)
            .Must(x => x == null || x.Length <= 1024 * 1024 * 10)
            .WithMessage("File size is larger than allowed")
            .When(x => x.Image != null)
            .Must(x => x == null || x.ContentType.Equals("image/jpg") || x.ContentType.Equals("image/jpeg"))
            .WithMessage("File type is not allowed");
    }
}
