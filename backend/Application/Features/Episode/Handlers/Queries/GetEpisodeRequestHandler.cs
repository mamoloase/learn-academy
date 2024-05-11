using System.Security.Claims;
using Application.Exceptions;
using Application.Features.Episode.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Episode.Handlers.Queries;
public class GetEpisodeRequestHandler : IRequestHandler<GetEpisodeRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetEpisodeRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetEpisodeRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var episode = await _unitOfWork.Episode.GetAsync(
            predicate: x => x.Id == request.Id);

        if (episode == null) throw new NotFoundException();

        var user = await _unitOfWork.User.GetAsync(
            predicate: x => x.Id == userId,
            include: i => i.Include(x => x.Courses));

        if (user == null)
            throw new UnauthorizedAccessException();

        if (!user.Courses.Any(x => x.Id == episode.CourseId))
            throw new AccessDeniedException();

        return new Response(true)
        {
            Result = episode.Document
        };
    }
}
