
using System.Security.Claims;
using Application.Exceptions;
using Application.Features.Teacher.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Features.Teacher.Handlers.Commands;
public class UpdateTeacherRequestHandler : IRequestHandler<UpdateTeacherRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeacherRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(UpdateTeacherRequest request, CancellationToken cancellationToken)
    {
        var userId = request.User.FindFirst(ClaimTypes.Sid).Value;

        var teacher = await _unitOfWork.Teacher.GetAsync(predicate: x => x.Id == request.Id);

        if (teacher == null) throw new NotFoundException();

        var user = await _unitOfWork.User.GetAsync(predicate: x => x.Id == userId);

        if (user == null) throw new UnauthorizedException();

        if (user.Role != RoleConstants.Admin && user.TeacherId != teacher.Id)
            throw new AccessDeniedException();

        var directory = Path.Join(LocationConstants.TeacherLocation, teacher.Id);

        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);

        var name = DateTime.Now.ToFileTime().ToString();

        if (request.Image != null)
        {
            var pathImage = Path.Join(directory, name + Path.GetExtension(request.Image.FileName));

            using (var stream = new FileStream(pathImage, FileMode.Create))
                await request.Image.CopyToAsync(stream);

            teacher.Image = pathImage.Replace(LocationConstants.RootLocation, string.Empty);
        }
        teacher.Description = request.Description;

        await _unitOfWork.Teacher.UpdateAsync(teacher);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true)
        {
            Result = _mapper.Map<TeacherModel>(teacher)
        };

    }
}
