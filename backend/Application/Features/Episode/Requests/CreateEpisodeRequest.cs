using System.Security.Claims;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Episode.Requests;
public class CreateEpisodeRequest : IRequest<Response>
{
    public string Name { get; set; }

    public string CourseId { get; set; }

    public IFormFile File { get; set; }
    public ClaimsPrincipal User { get; set; }

}


public class CreateEpisodeRequestValidator : AbstractValidator<CreateEpisodeRequest>
{
    public CreateEpisodeRequestValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(50).MinimumLength(3);

        RuleFor(x => x.File)
            .Cascade(CascadeMode.Stop).NotNull()

            .Must(x => x.Length <= 1024 * 1024 * 100)
            .WithMessage("File size is larger than allowed")
            .Must(x => x.ContentType.Equals("image/jpeg") 
                || x.ContentType.Equals("image/jpg") 
                || x.ContentType.Equals("application/zip") 
                || x.ContentType.Equals("video/mp4"))
            .WithMessage("File type is not allowed");
    }
}