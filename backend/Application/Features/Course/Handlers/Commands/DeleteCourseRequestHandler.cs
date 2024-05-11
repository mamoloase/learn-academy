using System.Security.Claims;
using Application.Exceptions;
using Application.Features.Course.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Models.Responses;
using MediatR;

namespace Application.Features.Course.Handlers.Commands;
public class DeleteCourseRequestHandler : IRequestHandler<DeleteCourseRequest, Response>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var course = await _unitOfWork.Course.GetAsync(predicate: x => x.Id == request.Id);

        if (course == null)
            throw new NotFoundException(
                string.Format(MessageConstants.ExceptionNotFound, nameof(request.Id)));

        var user = await _unitOfWork.User.GetAsync(predicate: x => x.Id == userId);

        if (user == null) throw new UnauthorizedException();

        if (user.Role != RoleConstants.Admin && course.TeacherId != user.TeacherId)
            throw new AccessDeniedException();

        await _unitOfWork.Course.DeleteAsync(course);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true);
    }
}
