using System.Security.Claims;
using Application.Features.Order.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Order.Handlers.Queries;
public class GetOrdersRequestHandler : IRequestHandler<GetOrdersRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetOrdersRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var orders = await _unitOfWork.Order.FilterAsync(
            predicate: x => x.UserId == userId,
            skip: (request.Page - 1) * request.Size, take: request.Size,
            orderBy: o => o.OrderByDescending(x => x.DateCreationAt)).ToListAsync();

        var count = await _unitOfWork.Order.CountAsync(predicate: x => x.UserId == userId);

        return new ResponsePagination
        {
            Result = _mapper.Map<List<OrderModel>>(orders),
            CountTotal = count,
            CountPage = (int)Math.Ceiling(count / (double)request.Size),
        };
    }
}
