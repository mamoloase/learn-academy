using System.Security.Claims;

using Application.Interfaces;
using Application.Exceptions;
using Application.Features.User.Requests;

using Domain.Models;
using Domain.Constants;
using Domain.Models.Responses;

using MediatR;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.User.Handlers.Commands;
public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var user = await _unitOfWork.User.GetAsync(predicate: x => x.Id == userId);

        if (user == null) throw new UnauthorizedException();

        var directory = Path.Join(LocationConstants.ProfileLocation, userId);

        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);

        var name = DateTime.Now.ToFileTime().ToString();
        if (request.Image != null)
        {
            var pathImage = Path.Join(directory, name + Path.GetExtension(request.Image.FileName));

            using (var stream = new FileStream(pathImage, FileMode.Create))
                await request.Image.CopyToAsync(stream);

            user.Image = pathImage.Replace(LocationConstants.RootLocation, string.Empty);
        }

        user.Name = request.Name;

        await _unitOfWork.User.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response(true)
        {
            Result = _mapper.Map<UserModel>(user)
        };
    }
}
