using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Services.WatchService
{
    public class WatchService : IWatchService
    {
        private static List<Watch> watches = new List<Watch>{
            new Watch{Id=1},
            new Watch{Id=2},
            new Watch{Id=3},
        };

        public Watch AddWatch(Watch watch)
        {
            watches.Add(watch);
            return watch;
        }

        public List<Watch> GetAllWatches()
        {
            return watches;
        }

        public Watch GetWatchById(int id)
        {
            if (id < 1)
            {
                return null;
            }
            return watches.FirstOrDefault(w => w.Id == id);
        }
    }
}