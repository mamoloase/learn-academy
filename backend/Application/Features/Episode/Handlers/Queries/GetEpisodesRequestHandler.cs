using Application.Features.Teacher.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Episode.Handlers.Queries;
public class GetEpisodesRequestHandler : IRequestHandler<GetEpisodesRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetEpisodesRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetEpisodesRequest request, CancellationToken cancellationToken)
    {
        var episodes = await _unitOfWork.Episode.FilterAsync(
            predicate: x => x.CourseId == request.CourseId).ToListAsync();

        return new Response(true)
        {
            Result = _mapper.Map<List<EpisodeModel>>(episodes)
        };
    }
}
