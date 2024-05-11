using System.Security.Claims;
using Application.Exceptions;
using Application.Features.Episode.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Responses;
using MediatR;

namespace Application.Features.Episode.Handlers.Commands;
public class CreateEpisodeRequestHandler : IRequestHandler<CreateEpisodeRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEpisodeRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(CreateEpisodeRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var course = await _unitOfWork.Course.GetAsync(
            predicate: x => x.Id == request.CourseId);

        if (course == null) throw new NotFoundException();

        var user = await _unitOfWork.User.GetAsync(
            predicate: x => x.Id == userId);

        if (user.Role != RoleConstants.Teacher || course.TeacherId != user.TeacherId)
            throw new AccessDeniedException();

        var episode = _mapper.Map<EpisodeEntity>(request);

        var directory = Path.Join(LocationConstants.CourseLocation, course.Id, "episodes");

        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);

        var name = DateTime.Now.ToFileTime().ToString();

        var pathDocument = Path.Join(directory, name + Path.GetExtension(request.File.FileName));

        using (var stream = new FileStream(pathDocument, FileMode.Create))
            await request.File.CopyToAsync(stream);
            
        episode.Document = pathDocument.Replace(LocationConstants.RootLocation, string.Empty);

        await _unitOfWork.Episode.InsertAsync(episode);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response(true);
    }
}
