using Domain.Constants;
using Domain.Models.Responses;

using Application.Interfaces;
using Application.Exceptions;
using Application.Features.Category.Requests;

using MediatR;

namespace Application.Features.Category.Handlers.Commands;
public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Response>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCategoryRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Category.GetAsync(predicate: x => x.Id == request.Id);
        if (category == null)
        {
            throw new NotFoundException(string.Format(MessageConstants.ExceptionNotFound,
                nameof(request.Id)));
        }

        await _unitOfWork.Category.DeleteAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true);

    }
}