using Application.Exceptions;
using Application.Features.Course.Requests;
using Application.Interfaces;
using Application.Profiles;
using AutoMapper;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Course.Handlers.Queries;
public class GetCourseCommentsRequestHandler : IRequestHandler<GetCourseCommentsRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetCourseCommentsRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetCourseCommentsRequest request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Course.GetAsync(predicate: x => x.Id == request.Id);

        if (course == null) throw new NotFoundException();

        var comments = await _unitOfWork.Comment.FilterAsync(
            predicate: x => x.CourseId == request.Id,
            skip: (request.Page - 1) * request.Size, take: request.Size).ToListAsync();

        var count = await _unitOfWork.Comment.CountAsync(x => x.CourseId == request.Id);

        return new ResponsePagination
        {
            Result = _mapper.Map<List<CommentModel>>(comments),
            CountTotal = count,
            CountPage = (int)Math.Ceiling(count / (double)request.Size),
        };
    }
}
