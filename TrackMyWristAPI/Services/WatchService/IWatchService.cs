using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Services.WatchService
{
    public interface IWatchService
    {
        Task<List<Watch>> GetAllWatches();
        Task<Watch> GetWatchById(int id);
        Task<Watch> AddWatch(Watch watch);
    }
}