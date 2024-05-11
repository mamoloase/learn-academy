using Application.Features.Course.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Profiles;
public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentEntity, CommentModel>().ReverseMap();
        CreateMap<CommentEntity, CreateCourseCommentRequest>().ReverseMap()
            .ForSourceMember(x => x.User, opt => opt.DoNotValidate());

    }

}