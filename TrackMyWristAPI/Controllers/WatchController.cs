using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Dtos;
using TrackMyWristAPI.Services.WatchService;

namespace TrackMyWristAPI.Controllers
{
    [ApiController]
    [Route("api/watches")]
    public class WatchController : ControllerBase
    {
        private readonly IWatchService _watchService;

        public WatchController(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetWatchDto>>> GetAllWatches()
        {
            return Ok(await _watchService.GetAllWatches());
        }

        [HttpGet("{id:int}", Name = "getWatchById")]
        public async Task<ActionResult<GetWatchDto>> GetWatchById(int id)
        {
            var existingWatch = await _watchService.GetWatchById(id);
            if (existingWatch == null)
            {
                return NotFound(existingWatch);
            }
            return Ok(existingWatch);
        }

        [HttpPost]
        public async Task<ActionResult<GetWatchDto>> AddWatch([FromBody] AddWatchDto watch)
        {
            var addedWatch = await _watchService.AddWatch(watch);
            return new CreatedAtRouteResult("getWatchById", new { Id = addedWatch.Id }, addedWatch);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GetWatchDto>> UpdateWatch(int id, [FromBody] UpdateWatchDto watch)
        {
            var updatedWatch = await _watchService.UpdateWatch(id, watch);
            if (updatedWatch == null)
            {
                return NotFound(updatedWatch);
            }
            return Ok(updatedWatch);
        }

        [HttpDelete("{id:int}")]
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