using MediatR;
using AutoMapper;

using Domain.Entities;
using Domain.Constants;

using Application.Interfaces;
using Application.Exceptions;
using Application.Extensions;
using Application.Features.Auth.Requests;
using Domain.Models;
using Domain.Models.Responses;


namespace Application.Features.User.Handlers.Commands;
public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        request.Email = request.Email.ToLower();

        var user = await _unitOfWork.User.GetAsync(x => x.Email == request.Email
            || x.Phone == request.Phone);

        if (user != null)
            throw new BadRequestException(user.Email == request.Email
                ? MessageConstants.ExceptionEmailAlreadyExists : MessageConstants.ExceptionPhoneAlreadyExists);

        user = _mapper.Map<UserEntity>(request);

        user.Password = HashExtensions.HashPassword(request.Password);

        await _unitOfWork.User.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true) { Result = _mapper.Map<UserModel>(user) };
    }
}