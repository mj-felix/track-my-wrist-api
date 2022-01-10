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

        [HttpGet("{id}")]
        public ActionResult<Watch> GetWatchById(int id)
        {
            return Ok(watches.FirstOrDefault(w => w.Id == id));
        }
    }
}