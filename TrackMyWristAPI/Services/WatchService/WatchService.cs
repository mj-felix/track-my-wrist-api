using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackMyWristAPI.Data;
using TrackMyWristAPI.Dtos.Watch;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Services.WatchService
{
    public class WatchService : IWatchService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public WatchService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetWatchDto>> GetAllWatches()
        {
            var watches = await _context.Watches.ToListAsync();
            return watches.Select(w => _mapper.Map<GetWatchDto>(w)).ToList();
        }

        public async Task<GetWatchDto> GetWatchById(int id)
        {
            if (id < 1)
            {
                return null;
            }
            var watch = await _context.Watches.FirstOrDefaultAsync(w => w.Id == id);
            if (watch == null)
            {
                return null;
            }
            return _mapper.Map<GetWatchDto>(watch);
        }

        public async Task<GetWatchDto> AddWatch(AddWatchDto watch)
        {
            {
                Watch watchToAdd = _mapper.Map<Watch>(watch);
                _context.Watches.Add(watchToAdd);
                await _context.SaveChangesAsync();
                return _mapper.Map<GetWatchDto>(watchToAdd);
            }
        }

        public async Task<GetWatchDto> UpdateWatch(int id, UpdateWatchDto watch)
        {
            if (id < 1)
            {
                return null;
            }
            Watch watchToUpdate = await _context.Watches.FirstOrDefaultAsync(w => w.Id == id);
            if (watchToUpdate == null)
            {
                return null;
            }
            watchToUpdate = _mapper.Map<UpdateWatchDto, Watch>(watch, watchToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWatchDto>(watchToUpdate);
        }

        public async Task<GetWatchDto> DeleteWatch(int id)
        {
            if (id < 1)
            {
                return null;
            }
            var watchToDelete = await _context.Watches.FirstOrDefaultAsync(w => w.Id == id);
            if (watchToDelete == null)
            {
                return null;
            }
            _context.Watches.Remove(watchToDelete);
            await _context.SaveChangesAsync();
            return new GetWatchDto();
        }
    }
}