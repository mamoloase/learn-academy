using MediatR;
using FluentValidation;
using Domain.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Teacher.Requests;
public class CreateTeacherRequest : IRequest<Response>
{
    public string UserId { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
} 

public class CreateTeacherRequestValidator : AbstractValidator<CreateTeacherRequest>
{
    public CreateTeacherRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
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