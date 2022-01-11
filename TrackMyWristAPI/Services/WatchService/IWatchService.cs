using System.Collections.Generic;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Services.WatchService
{
    public interface IWatchService
    {
        List<Watch> GetAllWatches();
        Watch GetWatchById(int id);
        Watch AddWatch(Watch watch);
    }
}