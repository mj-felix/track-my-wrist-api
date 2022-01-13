using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyWristAPI.Services.OwnershipService
{
    public interface IOwnershipService
    {
        public Task<bool> WatchBelongsToUser(int watchId);
        public Task<bool> WearingBelongsToWatchBelongsToUser(int wearingId, int watchId);
    }
}