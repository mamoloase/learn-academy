using System.Security.Claims;
using Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Episode.Requests;
public class GetEpisodeRequest : IRequest<Response>
{
    public string Id { get; set; }
    public ClaimsPrincipal User { get; set; }   
}
public class GetEpisodeRequestValidator : AbstractValidator<GetEpisodeRequest>
{
    public GetEpisodeRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

    }
}