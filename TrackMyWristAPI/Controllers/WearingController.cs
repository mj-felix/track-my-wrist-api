using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Dtos.Wearing;
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
        public WearingController(IWearingService wearingService)
        {
            _wearingService = wearingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<GetWearingDto>>> GetAllWearings(int watchId)
        {
            return Ok(await _wearingService.GetAllWearings(watchId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWearingDto>> AddWearing(int watchId, [FromBody] AddWearingDto wearing)
        {
            var addedWearing = await _wearingService.AddWearing(watchId, wearing);
            if (addedWearing == null)
            {
                return NotFound();
            }
            return Created("", addedWearing);
        }

    }
}