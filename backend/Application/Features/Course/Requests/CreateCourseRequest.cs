using MediatR;
using Domain.Enums;
using FluentValidation;
using Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Course.Requests;
public class CreateCourseRequest : IRequest<Response>
{
    public string Title { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }

    public CourseLevel Level { get; set; }
    public CourseStatus Status { get; set; }

    public ClaimsPrincipal User { get; set; }
    public IFormFile Image { get; set; }
    public IFormFile Video { get; set; }
}

public class CreateCourseRequestValidator : AbstractValidator<CreateCourseRequest>
{
    public CreateCourseRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
            .MaximumLength(50).MinimumLength(3);

        RuleFor(x => x.Description).NotEmpty()
            .MaximumLength(500).MinimumLength(3);

        RuleFor(x => x.Image)
            .Cascade(CascadeMode.Stop).NotNull()

            .Must(x => x.Length <= 1024 * 1024 * 10)
            .WithMessage("File size is larger than allowed")
            .Must(x => x.ContentType.Equals("image/jpeg") || x.ContentType.Equals("image/jpg"))
            .WithMessage("File type is not allowed");

        RuleFor(x => x.Video)
            .Cascade(CascadeMode.Stop).NotNull()
            .Must(x => x.Length <= 1024 * 1024 * 50)
            .WithMessage("File size is larger than allowed")
            .Must(x => x.ContentType.Equals("video/mp4"))
            .WithMessage("File type is not allowed");

    }
}