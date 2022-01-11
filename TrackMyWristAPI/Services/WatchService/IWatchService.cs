using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Services.WatchService
{
    public interface IWatchService
    {
        Task<ServiceResponse<List<Watch>>> GetAllWatches();
        Task<ServiceResponse<Watch>> GetWatchById(int id);
        Task<ServiceResponse<Watch>> AddWatch(Watch watch);
    }
}