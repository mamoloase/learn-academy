using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Profiles;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderEntity ,OrderModel>().ReverseMap();
    }
}
