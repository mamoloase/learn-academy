using System.Security.Claims;
using Application.Exceptions;
using Application.Features.Episode.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Episode.Handlers.Commands;
public class DeleteEpisodeRequestHandler : IRequestHandler<DeleteEpisodeRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEpisodeRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(DeleteEpisodeRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var episode = await _unitOfWork.Episode.GetAsync(
            predicate: x => x.Id == request.Id,
            include: i => i.Include(x => x.Course));

        if (episode == null) throw new NotFoundException();

        var user = await _unitOfWork.User.GetAsync(
            predicate: x => x.Id == userId);

        if (user.Role != RoleConstants.Admin &&
            (user.Role != RoleConstants.Teacher || episode.Course.TeacherId != user.TeacherId))
        {
            throw new AccessDeniedException();

        }

        await _unitOfWork.Episode.DeleteAsync(episode);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response(true);

    }
}
