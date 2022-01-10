using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchController : ControllerBase
    {
        private static Watch watch = new Watch();

        [HttpGet]
        public ActionResult<Watch> Get()
        {
            return Ok(watch);
        }
    }
}