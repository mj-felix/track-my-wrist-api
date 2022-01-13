using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyWristAPI.Dtos.Wearing;

namespace TrackMyWristAPI.Services.WearingService
{
    public interface IWearingService
    {
        Task<List<GetWearingDto>> GetAllWearings(int watchId);
        Task<GetWearingDto> AddWearing(int watchId, AddWearingDto wearing);
    }
}