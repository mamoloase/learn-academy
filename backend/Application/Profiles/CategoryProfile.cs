using Application.Features.Category.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Profiles;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryEntity ,CategoryModel>().ReverseMap();
        CreateMap<CategoryEntity ,CreateCategoryRequest>().ReverseMap();
        CreateMap<CategoryEntity ,UpdateCategoryRequest>().ReverseMap();
    }
}
