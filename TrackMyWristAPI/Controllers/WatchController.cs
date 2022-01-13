using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Dtos.Watch;
using TrackMyWristAPI.Services.OwnershipService;
using TrackMyWristAPI.Services.WatchService;

namespace TrackMyWristAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/watches")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class WatchController : ControllerBase
    {
        private readonly IWatchService _watchService;
        private readonly IOwnershipService _ownershipService;

        public WatchController(IWatchService watchService, IOwnershipService ownershipService)
        {
            _ownershipService = ownershipService;
            _watchService = watchService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<GetWatchDto>>> GetAllWatches()
        {
            return Ok(await _watchService.GetAllWatches());
        }

        [HttpGet("{id:int}", Name = "getWatchById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWatchDto>> GetWatchById(int id)
        {
            if (!await _ownershipService.WatchBelongsToUser(id))
            {
                return Forbid();
            }
            var existingWatch = await _watchService.GetWatchById(id);
            return Ok(existingWatch);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GetWatchDto>> AddWatch([FromBody] AddWatchDto watch)
        {
            var addedWatch = await _watchService.AddWatch(watch);
            return new CreatedAtRouteResult("getWatchById", new { Id = addedWatch.Id }, addedWatch);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWatchDto>> UpdateWatch(int id, [FromBody] UpdateWatchDto watch)
        {
            if (!await _ownershipService.WatchBelongsToUser(id))
            {
                return Forbid();
            }
            var updatedWatch = await _watchService.UpdateWatch(id, watch);
            return Ok(updatedWatch);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWatchDto>> DeleteWatch(int id)
        {
            if (!await _ownershipService.WatchBelongsToUser(id))
            {
                return Forbid();
            }
            await _watchService.DeleteWatch(id);
            return NoContent();
        }
    }
}