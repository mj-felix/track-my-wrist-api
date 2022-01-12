using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyWristAPI.Dtos;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Services.WatchService
{
    public interface IWatchService
    {
        Task<ServiceResponse<List<GetWatchDto>>> GetAllWatches();
        Task<ServiceResponse<GetWatchDto>> GetWatchById(int id);
        Task<ServiceResponse<GetWatchDto>> AddWatch(AddWatchDto watch);
        Task<ServiceResponse<GetWatchDto>> UpdateWatch(int id, UpdateWatchDto watch);
    }
}