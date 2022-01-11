using AutoMapper;
using TrackMyWristAPI.Dtos;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Watch, GetWatchDto>();
            CreateMap<AddWatchDto, Watch>();
        }
    }
}