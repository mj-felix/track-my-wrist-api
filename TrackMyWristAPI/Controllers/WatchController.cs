using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Dtos;
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
        public async Task<ActionResult<ServiceResponse<List<GetWatchDto>>>> GetAllWatches()
        {
            return Ok(await _watchService.GetAllWatches());
        }

        [HttpGet("{id:int}", Name = "getWatchById")]
        public async Task<ActionResult<ServiceResponse<GetWatchDto>>> GetWatchById(int id)
        {
            var existingWatch = await _watchService.GetWatchById(id);
            if (existingWatch.Data == null)
            {
                return NotFound(existingWatch);
            }
            return Ok(existingWatch);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetWatchDto>>> AddWatch([FromBody] AddWatchDto watch)
        {
            var addedWatch = await _watchService.AddWatch(watch);
            return new CreatedAtRouteResult("getWatchById", new { Id = addedWatch.Data.Id }, addedWatch);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetWatchDto>>> UpdateWatch(int id, [FromBody] UpdateWatchDto watch)
        {
            var updatedWatch = await _watchService.UpdateWatch(id, watch);
            if (updatedWatch.Data == null)
            {
                return NotFound(updatedWatch);
            }
            return Ok(updatedWatch);
        }
    }
}