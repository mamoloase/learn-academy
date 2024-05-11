using System.Security.Claims;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Episode.Requests;
public class UpdateEpisodeRequest : IRequest<Response>
{
    public string Id { get; set; }
    public string Name { get; set; }

    public IFormFile File { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public class UpdateEpisodeRequestValidator : AbstractValidator<UpdateEpisodeRequest>
{
    public UpdateEpisodeRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty()
            .MaximumLength(50).MinimumLength(3);

        RuleFor(x => x.File)
            .Cascade(CascadeMode.Stop)
            .Must(x => x == null || x.Length <= 1024 * 1024 * 100)
            .WithMessage("File size is larger than allowed")
            .When(x => x.File != null)
            .Must(x => x == null || x.ContentType.Equals("image/jpeg")
                || x.ContentType.Equals("image/jpg")
                || x.ContentType.Equals("application/zip")
                || x.ContentType.Equals("video/mp4"))
            .WithMessage("File type is not allowed");

    }
}