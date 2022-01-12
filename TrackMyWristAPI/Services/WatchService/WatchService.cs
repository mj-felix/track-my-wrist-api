using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        private readonly IHttpContextAccessor _httpContextAccessor;

        public WatchService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<List<GetWatchDto>> GetAllWatches()
        {
            var watches = await _context.Watches.Where(w => w.User.Id == GetUserId()).ToListAsync();
            return watches.Select(w => _mapper.Map<GetWatchDto>(w)).ToList();
        }

        public async Task<GetWatchDto> GetWatchById(int id)
        {
            if (id < 1)
            {
                return null;
            }
            var watch = await _context.Watches.FirstOrDefaultAsync(w => w.User.Id == GetUserId() && w.Id == id);
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
                watchToAdd.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
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
            Watch watchToUpdate = await _context.Watches.FirstOrDefaultAsync(w => w.User.Id == GetUserId() && w.Id == id);
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
            var watchToDelete = await _context.Watches.FirstOrDefaultAsync(w => w.User.Id == GetUserId() && w.Id == id);
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