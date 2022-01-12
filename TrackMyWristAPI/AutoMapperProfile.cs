using AutoMapper;
using TrackMyWristAPI.Dtos.Watch;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Watch, GetWatchDto>();
            CreateMap<AddWatchDto, Watch>();
            CreateMap<UpdateWatchDto, Watch>();
        }
    }
}