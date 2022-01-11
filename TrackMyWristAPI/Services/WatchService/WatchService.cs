using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackMyWristAPI.Dtos;
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
        private readonly IMapper _mapper;

        public WatchService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetWatchDto>> AddWatch(AddWatchDto watch)
        {
            var serviceResponse = new ServiceResponse<GetWatchDto>();
            Watch watchToAdd = _mapper.Map<Watch>(watch);
            watchToAdd.Id = watches.Max(w => w.Id) + 1;
            watches.Add(watchToAdd);
            serviceResponse.Data = _mapper.Map<GetWatchDto>(watchToAdd);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetWatchDto>>> GetAllWatches()
        {
            var serviceResponse = new ServiceResponse<List<GetWatchDto>>();
            serviceResponse.Data = watches.Select(w => _mapper.Map<GetWatchDto>(w)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetWatchDto>> GetWatchById(int id)
        {
            var serviceResponse = new ServiceResponse<GetWatchDto>();
            if (id < 1)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Watch not found";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<GetWatchDto>(watches.FirstOrDefault(w => w.Id == id));
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