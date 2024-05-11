using System.Security.Claims;
using Domain.Enums;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Course.Requests;
public class UpdateCourseRequest : IRequest<Response>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }

    public CourseLevel Level { get; set; }
    public CourseStatus Status { get; set; }

    public ClaimsPrincipal User { get; set; }
    public IFormFile Image { get; set; }
    public IFormFile Video { get; set; }
}
public class UpdateCourseRequestValidator : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty()
            .MaximumLength(50).MinimumLength(3);

        RuleFor(x => x.Description).NotEmpty()
            .MaximumLength(500).MinimumLength(3);

        RuleFor(x => x.Image)
            .Cascade(CascadeMode.Stop)
            .Must(x => x == null || x.Length <= 1024 * 1024 * 10)
            .WithMessage("File size is larger than allowed")
            .When(x => x.Image != null)
            .Must(x => x == null || x.ContentType.Equals("image/jpg") || x.ContentType.Equals("image/jpeg"))
            .WithMessage("File type is not allowed");

        RuleFor(x => x.Video)
            .Cascade(CascadeMode.Stop)
            .Must(x => x == null || x.Length <= 1024 * 1024 * 50)
            .WithMessage("File size is larger than allowed")
            .When(x => x.Video != null)
            .Must(x => x == null || x.ContentType.Equals("video/mp4"))
            .WithMessage("File type is not allowed");
    }
}