using Application.Exceptions;
using Application.Interfaces;
using Application.Features.Course.Requests;

using MediatR;
using AutoMapper;

using Domain.Models;
using Domain.Entities;
using Domain.Constants;
using Domain.Models.Responses;
using System.Security.Claims;

namespace Application.Features.Course.Handlers.Commands;
public class UpdateCourseRequestHandler : IRequestHandler<UpdateCourseRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(UpdateCourseRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var course = await _unitOfWork.Course.GetAsync(predicate: x => x.Id == request.Id);
        if (course == null)
        {
            throw new NotFoundException(string.Format(MessageConstants.ExceptionNotFound,
                nameof(request.Id)));
        }

        var user = await _unitOfWork.User.GetAsync(predicate: x => x.Id == userId);

        if (user == null) throw new UnauthorizedException();

        if (user.Role != RoleConstants.Admin && course.TeacherId != user.TeacherId)
            throw new AccessDeniedException();

        var directory = Path.Join(LocationConstants.CourseLocation, course.Id);

        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);

        var name = DateTime.Now.ToFileTime().ToString();

        if (request.Image != null)
        {
            var pathImage = Path.Join(directory, name + Path.GetExtension(request.Image.FileName));

            using (var stream = new FileStream(pathImage, FileMode.Create))
                await request.Image.CopyToAsync(stream);

            course.Image = pathImage.Replace(LocationConstants.RootLocation, string.Empty);
        }
        if (request.Video != null)
        {
            var pathVideo = Path.Join(directory, name + Path.GetExtension(request.Video.FileName));

            using (var stream = new FileStream(pathVideo, FileMode.Create))
                await request.Video.CopyToAsync(stream);
            course.Video = pathVideo.Replace(LocationConstants.RootLocation, string.Empty);
        }
        course.Title = request.Title;
        course.Price = request.Price;
        course.Level = request.Level;
        course.Status = request.Status;
        course.Description = request.Description;

        await _unitOfWork.Course.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true)
        {
            Result = _mapper.Map<CourseModel>(course)
        };
    }
}
