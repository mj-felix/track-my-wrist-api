using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<List<Watch>> GetAllWatches()
        {
            return Ok(_watchService.GetAllWatches());
        }

        [HttpGet("{id:int}", Name = "getWatchById")]
        public ActionResult<Watch> GetWatchById(int id)
        {
            var existingWatch = _watchService.GetWatchById(id);
            if (existingWatch == null)
            {
                return NotFound();
            }
            return Ok(existingWatch);
        }

        [HttpPost]
        public ActionResult<Watch> AddWatch(Watch watch)
        {
            var newWatch = _watchService.AddWatch(watch);
            return new CreatedAtRouteResult("getWatchById", new { Id = newWatch.Id }, newWatch);
        }
    }
}