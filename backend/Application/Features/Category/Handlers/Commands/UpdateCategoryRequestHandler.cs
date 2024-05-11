using Domain.Entities;

using Application.Exceptions;
using Application.Interfaces;
using Application.Features.Category.Requests;

using MediatR;
using AutoMapper;
using Domain.Constants;
using Domain.Models;
using Domain.Models.Responses;

namespace Application.Features.Category.Handlers.Commands;
public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCategoryRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Category.GetAsync(predicate: x => x.Id == request.Id);
        if (category == null)
        {
            throw new NotFoundException(string.Format(MessageConstants.ExceptionNotFound,
                nameof(request.Id)));
        }

        if (string.IsNullOrEmpty(request.ParentId))
            request.ParentId = null;

        if (string.IsNullOrEmpty(request.ParentId) == false
            && await _unitOfWork.Category.GetAsync(predicate: x => x.Id == request.ParentId) == null)
        {
            throw new NotFoundException(string.Format(MessageConstants.ExceptionNotFound,
                nameof(request.ParentId)));
        }

        category.Name = request.Name;
        category.ParentId = request.ParentId;
        category.Description = request.Description;

        await _unitOfWork.Category.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true)
        {
            Result = _mapper.Map<CategoryModel>(category)
        };

    }
}