using Application.Features.Teacher.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Profiles;
public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<TeacherModel, TeacherEntity>().ReverseMap();
        CreateMap<TeacherEntity, CreateTeacherRequest>().ReverseMap();
        CreateMap<TeacherEntity, UpdateTeacherRequest>().ReverseMap();
    }
}
