
using System.Security.Claims;
using Application.Exceptions;
using Application.Features.Episode.Requests;
using Application.Interfaces;

using Domain.Constants;
using Domain.Models.Responses;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Episode.Handlers.Commands;
public class UpdateEpisodeRequestHandler : IRequestHandler<UpdateEpisodeRequest, Response>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEpisodeRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(UpdateEpisodeRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var episode = await _unitOfWork.Episode.GetAsync(
            predicate: x => x.Id == request.Id,
            include: i => i.Include(x => x.Course));

        if (episode == null) throw new NotFoundException();

        var user = await _unitOfWork.User.GetAsync(
            predicate: x => x.Id == userId);

        if (user.Role != RoleConstants.Admin && (user.Role != RoleConstants.Teacher || episode.Course.TeacherId != user.TeacherId))
            throw new AccessDeniedException();

        var directory = Path.Join(LocationConstants.CourseLocation, episode.Course.Id, "episodes");

        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);

        if (request.File != null)
        {
            var name = DateTime.Now.ToFileTime().ToString();

            var pathDocument = Path.Join(directory, name + Path.GetExtension(request.File.FileName));

            using (var stream = new FileStream(pathDocument, FileMode.Create))
                await request.File.CopyToAsync(stream);

            episode.Document = pathDocument.Replace(LocationConstants.RootLocation, string.Empty);
        }

        episode.Name = request.Name;

        await _unitOfWork.Episode.UpdateAsync(episode);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response(true);

    }
}
