using System.Security.Claims;
using Application.Exceptions;
using Application.Extensions;
using Application.Features.User.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models.Responses;
using MediatR;

namespace Application.Features.User.Handlers.Commands;
public class ChangeUserPasswordRequestHandler : IRequestHandler<ChangeUserPasswordRequest, Response>
{
    private readonly IUnitOfWork _unitOfWork;
    public ChangeUserPasswordRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;
        var user = await _unitOfWork.User.GetAsync(x => x.Id == userId);

        if (user == null) throw new UnauthorizedAccessException();

        if (!HashExtensions.VerifyHashedPassword(user.Password, request.Password))
            throw new BadRequestException();

        user.Password = HashExtensions.HashPassword(request.NewPassword);

        await _unitOfWork.User.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true);

    }
}
