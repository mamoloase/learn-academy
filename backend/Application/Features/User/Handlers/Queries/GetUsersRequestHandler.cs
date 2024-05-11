
using Application.Features.User.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.Handlers.Queries;
public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.User.FilterAsync(
            skip: (request.Page - 1) * request.Size, take: request.Size).ToListAsync();

        var count = await _unitOfWork.User.CountAsync();

        return new ResponsePagination
        {
            Result = _mapper.Map<List<UserModel>>(users),
            CountTotal = count,
            CountPage = (int)Math.Ceiling(count / (double)request.Size),
        };
    }
}
