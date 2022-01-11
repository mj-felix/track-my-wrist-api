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

        public async Task<ServiceResponse<Watch>> AddWatch(Watch watch)
        {
            var serviceResponse = new ServiceResponse<Watch>();
            watches.Add(watch);
            serviceResponse.Data = watch;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Watch>>> GetAllWatches()
        {
            var serviceResponse = new ServiceResponse<List<Watch>>();
            serviceResponse.Data = watches;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Watch>> GetWatchById(int id)
        {
            var serviceResponse = new ServiceResponse<Watch>();
            if (id < 1)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Watch not found";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            serviceResponse.Data = watches.FirstOrDefault(w => w.Id == id);
            if (serviceResponse.Data != null)
            {
                return serviceResponse;
            }
            else
            {
                serviceResponse.Message = "Watch not found";
                serviceResponse.Success = false;
                return serviceResponse;
            }
        }
    }
}