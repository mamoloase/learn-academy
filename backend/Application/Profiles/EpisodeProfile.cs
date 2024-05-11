using Application.Features.Episode.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Profiles;
public class EpisodeProfile : Profile
{
    public EpisodeProfile()
    {
        CreateMap<EpisodeEntity, EpisodeModel>().ReverseMap();
        CreateMap<EpisodeEntity, CreateEpisodeRequest>().ReverseMap();

        
    }
}
