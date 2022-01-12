using System;
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
            try
            {
                var serviceResponse = new ServiceResponse<GetWatchDto>();
                Watch watchToAdd = _mapper.Map<Watch>(watch);
                watchToAdd.Id = watches.Max(w => w.Id) + 1;
                watches.Add(watchToAdd);
                serviceResponse.Data = _mapper.Map<GetWatchDto>(watchToAdd);
                return serviceResponse;
            }
            catch (Exception e)
            {
                return returnErrorServiceResponse(e.Message);
            }
        }

        public async Task<ServiceResponse<List<GetWatchDto>>> GetAllWatches()
        {
            var serviceResponse = new ServiceResponse<List<GetWatchDto>>();
            serviceResponse.Data = watches.Select(w => _mapper.Map<GetWatchDto>(w)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetWatchDto>> GetWatchById(int id)
        {
            try
            {
                if (id < 1)
                {
                    return returnErrorServiceResponse("Watch not found");
                }
                var serviceResponse = new ServiceResponse<GetWatchDto>();
                serviceResponse.Data = _mapper.Map<GetWatchDto>(watches.FirstOrDefault(w => w.Id == id));
                if (serviceResponse.Data == null)
                {
                    return returnErrorServiceResponse("Watch not found");
                }
                return serviceResponse;
            }
            catch (Exception e)
            {
                return returnErrorServiceResponse(e.Message);
            }
        }

        public async Task<ServiceResponse<GetWatchDto>> UpdateWatch(int id, UpdateWatchDto watch)
        {
            try
            {
                if (id < 1)
                {
                    return returnErrorServiceResponse("Watch not found");
                }
                Watch watchToUpdate = watches.FirstOrDefault(w => w.Id == id);
                if (watchToUpdate == null)
                {
                    return returnErrorServiceResponse("Watch not found");
                }
                watchToUpdate = _mapper.Map<UpdateWatchDto, Watch>(watch, watchToUpdate);
                var serviceResponse = new ServiceResponse<GetWatchDto>();
                serviceResponse.Data = _mapper.Map<GetWatchDto>(watchToUpdate);
                return serviceResponse;
            }
            catch (Exception e)
            {
                return returnErrorServiceResponse(e.Message);
            }
        }

        private ServiceResponse<GetWatchDto> returnErrorServiceResponse(string message)
        {
            return new ServiceResponse<GetWatchDto>
            {
                Data = null,
                Message = message,
                Success = false
            };
        }
    }
}