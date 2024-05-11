using AutoMapper;
using Application.Features.Auth.Requests;

using Domain.Entities;
using Domain.Models;

namespace Application.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity ,UserModel>().ReverseMap();
        CreateMap<UserEntity ,CreateUserRequest>().ReverseMap();
    }
}
