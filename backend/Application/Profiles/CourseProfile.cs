using AutoMapper;

using Application.Features.Course.Requests;

using Domain.Models;
using Domain.Entities;

namespace Application.Profiles;
public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CourseEntity ,CourseModel>().ReverseMap();
        CreateMap<CourseEntity ,CreateCourseRequest>().ReverseMap();
        CreateMap<CourseEntity ,UpdateCourseRequest>().ReverseMap();
    }
}
