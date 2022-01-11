using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Models;
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
        public async Task<ActionResult<List<Watch>>> GetAllWatches()
        {
            return Ok(await _watchService.GetAllWatches());
        }

        [HttpGet("{id:int}", Name = "getWatchById")]
        public async Task<ActionResult<Watch>> GetWatchById(int id)
        {
            var existingWatch = await _watchService.GetWatchById(id);
            if (existingWatch == null)
            {
                return NotFound();
            }
            return Ok(existingWatch);
        }

        [HttpPost]
        public async Task<ActionResult<Watch>> AddWatch(Watch watch)
        {
            var newWatch = await _watchService.AddWatch(watch);
            return new CreatedAtRouteResult("getWatchById", new { Id = newWatch.Id }, newWatch);
        }
    }
}