using AutoMapper;
using TrackMyWristAPI.Dtos.Watch;
using TrackMyWristAPI.Dtos.Wearing;
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

            CreateMap<Wearing, GetWearingDto>();
            CreateMap<AddWearingDto, Wearing>();
            CreateMap<UpdateWearingDto, Wearing>();
        }
    }
}