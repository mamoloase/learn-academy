using MediatR;
using AutoMapper;

using Domain.Models;
using Domain.Models.Responses;

using Application.Interfaces;
using Application.Features.Course.Requests;

using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Application.Features.Course.Handlers.Queries;
public class GetCoursesRequestHandler : IRequestHandler<GetCoursesRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetCoursesRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetCoursesRequest request, CancellationToken cancellationToken)
    {

        var courses = await _unitOfWork.Course.FilterAsync(
            predicate: x =>
                !string.IsNullOrEmpty(request.CategoryId) ? x.CategoryId == request.CategoryId
                : !string.IsNullOrEmpty(request.CategoryId) ? x.TeacherId == request.TetacherId
                : true,

            skip: (request.Page - 1) * request.Size, take: request.Size,
            orderBy: o =>
                request.OrderBy == CourseOrderBy.Date ? o.OrderBy(x => x.DateCreationAt) :
                request.OrderBy == CourseOrderBy.DateDescending ? o.OrderByDescending(x => x.DateCreationAt) :
                request.OrderBy == CourseOrderBy.Price ? o.OrderBy(x => x.Price) :
                o.OrderByDescending(x => x.Price)
            ).ToListAsync();

        var count = await _unitOfWork.Course.CountAsync(predicate: x =>
                !string.IsNullOrEmpty(request.CategoryId) ? x.CategoryId == request.CategoryId
                : !string.IsNullOrEmpty(request.CategoryId) ? x.TeacherId == request.TetacherId
                : true);

        return new ResponsePagination
        {
            Result = _mapper.Map<List<CourseModel>>(courses),
            CountTotal = count,
            CountPage = (int)Math.Ceiling(count / (double)request.Size),
        };

    }
}
