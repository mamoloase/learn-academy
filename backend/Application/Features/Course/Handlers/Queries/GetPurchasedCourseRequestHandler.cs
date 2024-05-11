using System.Security.Claims;
using Application.Features.Course.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Course.Handlers.Queries;
public class GetPurchasedCourseRequestHandler : IRequestHandler<GetPurchasedCourseRequest, Response>
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetPurchasedCourseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetPurchasedCourseRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var user = await _unitOfWork.User.GetAsync(
            predicate: x => x.Id == userId, include: x => x.Include(i => i.Courses));

        if (user == null) throw new UnauthorizedAccessException();

        var count = user.Courses.Count();

        return new ResponsePagination
        {
            Result = _mapper.Map<List<CourseModel>>(user.Courses),
            CountTotal = count,
            CountPage = (int)Math.Ceiling(count / (double)request.Size),
        };
    }
}
