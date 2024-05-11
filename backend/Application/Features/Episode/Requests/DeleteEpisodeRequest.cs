using System.Security.Claims;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Episode.Requests;
public class DeleteEpisodeRequest : IRequest<Response>
{
    public string Id { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public class DeleteEpisodeRequestValidator : AbstractValidator<DeleteEpisodeRequest>
{
    public DeleteEpisodeRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

    }
}