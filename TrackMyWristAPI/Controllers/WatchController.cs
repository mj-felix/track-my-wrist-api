using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Dtos.Watch;
using TrackMyWristAPI.Services.WatchService;

namespace TrackMyWristAPI.Controllers
{
    [ApiController]
    [Route("api/watches")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class WatchController : ControllerBase
    {
        private readonly IWatchService _watchService;

        public WatchController(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetWatchDto>>> GetAllWatches()
        {
            return Ok(await _watchService.GetAllWatches());
        }

        [HttpGet("{id:int}", Name = "getWatchById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWatchDto>> GetWatchById(int id)
        {
            var existingWatch = await _watchService.GetWatchById(id);
            if (existingWatch == null)
            {
                return NotFound();
            }
            return Ok(existingWatch);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetWatchDto>> AddWatch([FromBody] AddWatchDto watch)
        {
            var addedWatch = await _watchService.AddWatch(watch);
            return new CreatedAtRouteResult("getWatchById", new { Id = addedWatch.Id }, addedWatch);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWatchDto>> UpdateWatch(int id, [FromBody] UpdateWatchDto watch)
        {
            var updatedWatch = await _watchService.UpdateWatch(id, watch);
            if (updatedWatch == null)
            {
                return NotFound();
            }
            return Ok(updatedWatch);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWatchDto>> DeleteWatch(int id)
        {
            var deletedWatch = await _watchService.DeleteWatch(id);
            if (deletedWatch == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}