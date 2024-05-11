using MediatR;
using AutoMapper;

using Application.Exceptions;
using Application.Interfaces;
using Application.Features.Course.Requests;

using Domain.Models;
using Domain.Entities;
using Domain.Constants;
using Domain.Models.Responses;
using System.Security.Claims;

namespace Application.Features.Course.Handlers.Commands;
public class CreateCourseRequestHandler : IRequestHandler<CreateCourseRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;
        var user = await _unitOfWork.User.GetAsync(predicate: x => x.Id == userId);

        if (user == null) throw new UnauthorizedException();

        if (string.IsNullOrEmpty(user.TeacherId))
            throw new AccessDeniedException();

        var course = _mapper.Map<CourseEntity>(request);
        var directory = Path.Join(LocationConstants.CourseLocation, course.Id);

        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);

        var name = DateTime.Now.ToFileTime().ToString();

        var pathImage = Path.Join(directory, name + Path.GetExtension(request.Image.FileName));
        var pathVideo = Path.Join(directory, name + Path.GetExtension(request.Video.FileName));

        using (var stream = new FileStream(pathImage, FileMode.Create))
            await request.Image.CopyToAsync(stream);

        using (var stream = new FileStream(pathVideo, FileMode.Create))
            await request.Video.CopyToAsync(stream);

        course.Image = pathImage.Replace(LocationConstants.RootLocation ,string.Empty);
        course.Video = pathVideo.Replace(LocationConstants.RootLocation ,string.Empty);

        course.TeacherId = user.TeacherId;

        await _unitOfWork.Course.InsertAsync(course);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true)
        {
            Result = _mapper.Map<CourseModel>(course)
        };
    }
}
