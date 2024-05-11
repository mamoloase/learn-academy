using Application.Features.Category.Requests;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Category.Handlers.Queries;
public class GetCategoriesRequestHandler : IRequestHandler<GetCategoriesRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetCategoriesRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Category.FilterAsync().ToListAsync();

        return new Response(true)
        {
            Result = _mapper.Map<List<CategoryModel>>(categories)
        };
    }
}
