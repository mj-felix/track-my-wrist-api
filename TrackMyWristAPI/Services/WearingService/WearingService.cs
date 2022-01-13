using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TrackMyWristAPI.Data;
using TrackMyWristAPI.Dtos.Wearing;
using TrackMyWristAPI.Models;
using TrackMyWristAPI.Services.UserService;

namespace TrackMyWristAPI.Services.WearingService
{
    public class WearingService : IWearingService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public WearingService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetWearingDto> AddWearing(int watchId, AddWearingDto wearing)
        {
            Wearing wearingToAdd = _mapper.Map<Wearing>(wearing);
            wearingToAdd.Watch = await _context.Watches.FirstOrDefaultAsync(w => w.Id == watchId);
            _context.Wearings.Add(wearingToAdd);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWearingDto>(wearingToAdd);
        }

        public async Task<GetWearingDto> UpdateWearing(int id, UpdateWearingDto wearing)
        {
            Wearing wearingToUpdate = await _context.Wearings.FirstOrDefaultAsync(w => w.Id == id);
            wearingToUpdate = _mapper.Map<UpdateWearingDto, Wearing>(wearing, wearingToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWearingDto>(wearingToUpdate);
        }
        public async Task<GetWearingDto> DeleteWearing(int id)
        {
            var wearingToDelete = await _context.Wearings.FirstOrDefaultAsync(w => w.Id == id);
            _context.Wearings.Remove(wearingToDelete);
            await _context.SaveChangesAsync();
            return new GetWearingDto();
        }

        public async Task<List<GetWearingDto>> GetAllWearings(int watchId)
        {
            var wearings = await _context.Wearings.Where(w => w.Watch.Id == watchId).ToListAsync();
            return wearings.Select(w => _mapper.Map<GetWearingDto>(w)).ToList();
        }

    }
}