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

        public async Task<GetWatchDto> AddWatch(AddWatchDto watch)
        {
            {
                Watch watchToAdd = _mapper.Map<Watch>(watch);
                watchToAdd.Id = watches.Max(w => w.Id) + 1;
                watches.Add(watchToAdd);
                return _mapper.Map<GetWatchDto>(watchToAdd);
            }
        }

        public async Task<GetWatchDto> DeleteWatch(int id)
        {
            if (id < 1)
            {
                return null;
            }
            var existingWatch = watches.FirstOrDefault(w => w.Id == id);
            if (existingWatch == null)
            {
                return null;
            }
            watches.Remove(existingWatch);
            return new GetWatchDto();
        }

        public async Task<List<GetWatchDto>> GetAllWatches()
        {
            return watches.Select(w => _mapper.Map<GetWatchDto>(w)).ToList();
        }

        public async Task<GetWatchDto> GetWatchById(int id)
        {
            if (id < 1)
            {
                return null;
            }
            var existingWatch = _mapper.Map<GetWatchDto>(watches.FirstOrDefault(w => w.Id == id));
            if (existingWatch == null)
            {
                return null;
            }
            return existingWatch;
        }

        public async Task<GetWatchDto> UpdateWatch(int id, UpdateWatchDto watch)
        {
            if (id < 1)
            {
                return null;
            }
            Watch watchToUpdate = watches.FirstOrDefault(w => w.Id == id);
            if (watchToUpdate == null)
            {
                return null;
            }
            watchToUpdate = _mapper.Map<UpdateWatchDto, Watch>(watch, watchToUpdate);
            return _mapper.Map<GetWatchDto>(watchToUpdate);
        }
    }
}