
using Application.Features.Course.Requests;
using Application.Interfaces;

using Domain.Models.Responses;

using MediatR;
using AutoMapper;
using Application.Exceptions;

namespace Application.Features.Course.Handlers.Commands;
public class DeleteCourseCommentRequestHandler : IRequestHandler<DeleteCourseCommentRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommentRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(DeleteCourseCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comment.GetAsync(predicate: x => x.Id == request.Id);

        if (comment == null)
            throw new NotFoundException();

        await _unitOfWork.Comment.DeleteAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true);
    }
}
