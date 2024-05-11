
using System.Security.Claims;
using Application.Exceptions;
using Application.Features.Course.Requests;
using Application.Interfaces;
using Application.Profiles;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Course.Handlers.Commands;
public class CreateCourseCommentRequestHandler : IRequestHandler<CreateCourseCommentRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommentRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(CreateCourseCommentRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.AnswerId))
            request.AnswerId = null;

        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var course = await _unitOfWork.Course.GetAsync(predicate: x => x.Id == request.CourseId);

        if (course == null) throw new NotFoundException();

        if (string.IsNullOrEmpty(request.AnswerId) == false
            && await _unitOfWork.Comment.GetAsync(predicate: x => x.Id == request.AnswerId) == null)
        {
            throw new NotFoundException(string.Format(MessageConstants.ExceptionNotFound,
                nameof(request.AnswerId)));
        }

        var user = await _unitOfWork.User.GetAsync(
            predicate: x => x.Id == userId,
            include: i => i.Include(x => x.Courses));

        if (user == null) throw new UnauthorizedAccessException();

        if (string.IsNullOrEmpty(request.AnswerId) == false
            && (user.Role != RoleConstants.Teacher || user.TeacherId != course.TeacherId))
        {
            throw new AccessDeniedException();
        }
        await _unitOfWork.Comment.InsertAsync(
            new CommentEntity
            {
                UserId = userId,
                AnswerId = request.AnswerId,
                CourseId = request.CourseId,
                Message = request.Message,
            }
        );
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response(true);
    }
}
