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
        private readonly IUserService _userService;

        public WearingService(IMapper mapper, DataContext context, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<GetWearingDto> AddWearing(int watchId, AddWearingDto wearing)
        {
            Wearing wearingToAdd = _mapper.Map<Wearing>(wearing);
            wearingToAdd.Watch = await _context.Watches.FirstOrDefaultAsync(w => w.User.Id == _userService.GetUserId() && w.Id == watchId);
            if (wearingToAdd.Watch == null)
            {
                return null;
            }
            _context.Wearings.Add(wearingToAdd);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWearingDto>(wearingToAdd);
        }

        public async Task<List<GetWearingDto>> GetAllWearings(int watchId)
        {
            var wearings = await _context.Wearings.Where(w => w.Watch.User.Id == _userService.GetUserId() && w.Watch.Id == watchId).ToListAsync();
            return wearings.Select(w => _mapper.Map<GetWearingDto>(w)).ToList();
        }
    }
}