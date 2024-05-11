using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Application.Exceptions;
using Application.Features.Order.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Order.Handlers.Commands;
public class CreateOrdersRequestHandler : IRequestHandler<CreateOrdersRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateOrdersRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(CreateOrdersRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var course = await _unitOfWork.Course.GetAsync(x => x.Id == request.CourseId);

        if (course == null) throw new NotFoundException();

        var user = await _unitOfWork.User.GetAsync(
            predicate: x => x.Id == userId,
            include: i => i.Include(x => x.Orders));

        if (user == null) throw new UnauthorizedAccessException();
        var order = user.Orders.SingleOrDefault(
            x => x.CourseId == request.CourseId && x.Status != OrderStatus.Canceled);

        if (order != null)
            return new Response(true);

        await _unitOfWork.Order.InsertAsync(
            new OrderEntity
            {
                CourseId = request.CourseId,
                Status = OrderStatus.Pending,
                UserId = userId,
            }
        );
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response(true);
    }
}
