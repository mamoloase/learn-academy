using System.Net.Http.Headers;
using Application.Exceptions;
using Application.Features.Teacher.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models.Responses;
using MediatR;

namespace Application.Features.Teacher.Handlers.Commands;
public class DeleteTeacherRequestHandler : IRequestHandler<DeleteTeacherRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTeacherRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(DeleteTeacherRequest request, CancellationToken cancellationToken)
    {
        var teacher = await _unitOfWork.Teacher.GetAsync(predicate: x => x.Id == request.Id);

        if (teacher == null) throw new NotFoundException();

        await _unitOfWork.Teacher.DeleteAsync(teacher);
        await _unitOfWork.SaveChangesAsync();
        return new Response(true);
    }
}
