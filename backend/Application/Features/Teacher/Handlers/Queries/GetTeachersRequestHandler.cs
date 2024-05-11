using Application.Features.Teacher.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Teacher.Handlers.Queries;
public class GetTeachersRequestHandler : IRequestHandler<GetTeachersRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTeachersRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetTeachersRequest request, CancellationToken cancellationToken)
    {
        var teachers = await _unitOfWork.Teacher.FilterAsync(
            skip: (request.Page - 1) * request.Size, take: request.Size).ToListAsync();

        var count = await _unitOfWork.Teacher.CountAsync();

        return new ResponsePagination
        {
            Result = _mapper.Map<List<TeacherModel>>(teachers),
            CountTotal = count,
            CountPage = (int)Math.Ceiling(count / (double)request.Size),
        };

    }
}
