using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Dtos.Wearing;
using TrackMyWristAPI.Services.OwnershipService;
using TrackMyWristAPI.Services.UserService;
using TrackMyWristAPI.Services.WearingService;

namespace TrackMyWristAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/watches/{watchId}/wearings")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class WearingController : ControllerBase
    {
        private readonly IWearingService _wearingService;
        private readonly IOwnershipService _ownershipService;
        public WearingController(IWearingService wearingService, IOwnershipService ownershipService)
        {
            _ownershipService = ownershipService;
            _wearingService = wearingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<GetWearingDto>>> GetAllWearings(int watchId)
        {
            if (!await _ownershipService.WatchBelongsToUser(watchId))
            {
                return Forbid();
            }
            return Ok(await _wearingService.GetAllWearings(watchId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetWearingDto>> AddWearing(int watchId, [FromBody] AddWearingDto wearing)
        {
            if (!await _ownershipService.WatchBelongsToUser(watchId))
            {
                return Forbid();
            }
            var addedWearing = await _wearingService.AddWearing(watchId, wearing);
            return Created("", addedWearing);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetWearingDto>> UpdateWearing(int watchId, int id, [FromBody] UpdateWearingDto wearing)
        {
            if (!await _ownershipService.WearingBelongsToWatchBelongsToUser(id, watchId))
            {
                return Forbid();
            }
            var updatedWearing = await _wearingService.UpdateWearing(id, wearing);
            return Ok(updatedWearing);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetWearingDto>> DeleteWearing(int watchId, int id)
        {
            if (!await _ownershipService.WearingBelongsToWatchBelongsToUser(id, watchId))
            {
                return Forbid();
            }
            await _wearingService.DeleteWearing(id);
            return NoContent();
        }

    }
}