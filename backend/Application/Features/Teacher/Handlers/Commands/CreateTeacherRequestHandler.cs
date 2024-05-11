
using Application.Interfaces;
using Application.Exceptions;
using Application.Features.Teacher.Requests;

using Domain.Models;
using Domain.Entities;
using Domain.Models.Responses;

using MediatR;
using AutoMapper;
using Domain.Constants;

namespace Application.Features.Teacher.Handlers.Commands;
public class CreateTeacherRequestHandler : IRequestHandler<CreateTeacherRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeacherRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(CreateTeacherRequest request, CancellationToken cancellationToken)
    {
        var teacher = await _unitOfWork.Teacher.GetAsync(x => x.User.Id == request.UserId);

        if (teacher != null)
            return new Response(true) { Result = _mapper.Map<TeacherModel>(teacher) };

        var user = await _unitOfWork.User.GetAsync(x => x.Id == request.UserId);

        if (user == null) throw new NotFoundException();

        teacher = _mapper.Map<TeacherEntity>(request);

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
        user.TeacherId = teacher.Id;
        user.Role = RoleConstants.Teacher;

        await _unitOfWork.Teacher.InsertAsync(teacher);
        await _unitOfWork.User.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();



        return new Response(true) { Result = _mapper.Map<TeacherModel>(teacher) };

    }
}
