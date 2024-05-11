
using MediatR;

using Application.Interfaces;
using Application.Exceptions;
using Application.Features.Auth.Requests;
using Application.Features.Auth.Responses;
using Application.Extensions;
using Domain.Configurations;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Domain.Constants;
using Domain.Models.Responses;

namespace Application.Features.Auth.Handlers.Commands;
public class SignInRequestHandler : IRequestHandler<SignInRequest, Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenConfiguration _tokenConfiguration;
    public SignInRequestHandler(IUnitOfWork unitOfWork, IOptions<TokenConfiguration> tokenConfiguration)
    {
        _unitOfWork = unitOfWork;
        _tokenConfiguration = tokenConfiguration.Value;
    }
    public async Task<Response> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        request.Email = request.Email.ToLower();

        var user = await _unitOfWork.User.GetAsync(predicate: x => x.Email == request.Email);

        if (user == null || !HashExtensions.VerifyHashedPassword(user.Password, request.Password))
            throw new BadRequestException(
                string.Format(MessageConstants.ExceptionNotFound, "User"));

        (var accessToken, var accessTokenExpirationAt) = TokenExtensions.GenerateAccessToken(
            _tokenConfiguration, [
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Sid, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            ]);

        var result = new SignInResponse
        {
            AccessToken = accessToken,
            AccessTokenExpirationAt = accessTokenExpirationAt
        };
        return new Response(true) { Result = result };
    }
}
