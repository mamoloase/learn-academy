using MediatR;
using Domain.Models.Responses;
using FluentValidation;

namespace Application.Features.Teacher.Requests;
public class GetEpisodesRequest : IRequest<Response>
{
    public string CourseId { get; set; }
}
public class GetEpisodesRequestValidator : AbstractValidator<GetEpisodesRequest>
{
    public GetEpisodesRequestValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();

    }
}