using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyWristAPI.Dtos.Watch;

namespace TrackMyWristAPI.Services.WatchService
{
    public interface IWatchService
    {
        Task<List<GetWatchDto>> GetAllWatches();
        Task<GetWatchDto> GetWatchById(int id);
        Task<GetWatchDto> AddWatch(AddWatchDto watch);
        Task<GetWatchDto> UpdateWatch(int id, UpdateWatchDto watch);
        Task<GetWatchDto> DeleteWatch(int id);
    }
}