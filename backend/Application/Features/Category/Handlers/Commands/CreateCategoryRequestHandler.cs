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
public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCategoryRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.ParentId))
            request.ParentId = null;

        if (string.IsNullOrEmpty(request.ParentId) == false
            && await _unitOfWork.Category.GetAsync(predicate: x => x.Id == request.ParentId) == null)
        {
            throw new NotFoundException(string.Format(MessageConstants.ExceptionNotFound,
                nameof(request.ParentId)));
        }

        var category = _mapper.Map<CategoryEntity>(request);

        await _unitOfWork.Category.InsertAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return new Response(true)
        {
            Result = _mapper.Map<CategoryModel>(category)
        };

    }
}