using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackMyWristAPI.Data;
using TrackMyWristAPI.Services.UserService;

namespace TrackMyWristAPI.Services.OwnershipService
{
    public class OwnershipService : IOwnershipService
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        public OwnershipService(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<bool> WatchBelongsToUser(int watchId)
        {
            var watch = await _context.Watches.FirstOrDefaultAsync(w => w.Id == watchId && w.User.Id == _userService.GetUserId());
            if (watch == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> WearingBelongsToWatchBelongsToUser(int wearingId, int watchId)
        {
            var watch = await _context.Wearings.FirstOrDefaultAsync(w => w.Id == wearingId && w.Watch.Id == watchId && w.Watch.User.Id == _userService.GetUserId());
            if (watch == null)
            {
                return false;
            }
            return true;
        }
    }
}