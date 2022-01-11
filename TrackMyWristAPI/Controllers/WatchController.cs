using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Controllers
{
    [ApiController]
    [Route("api/watches")]
    public class WatchController : ControllerBase
    {
        private static List<Watch> watches = new List<Watch>{
            new Watch{Id=1},
            new Watch{Id=2},
            new Watch{Id=3},
        };

        [HttpGet]
        public ActionResult<List<Watch>> GetAllWatches()
        {
            return Ok(watches);
        }

        [HttpGet("{id:int}", Name = "getWatchById")]
        public ActionResult<Watch> GetWatchById(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            return Ok(watches.FirstOrDefault(w => w.Id == id));
        }

        [HttpPost]
        public ActionResult<Watch> AddWatch(Watch watch)
        {
            watches.Add(watch);
            return new CreatedAtRouteResult("getWatchById", new { Id = watch.Id }, watch);
        }
    }
}