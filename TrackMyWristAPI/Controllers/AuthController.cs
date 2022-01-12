using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Data;
using TrackMyWristAPI.Dtos.User;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthServiceResponse>> Register(RegisterUserDto request)
        {
            var response = await _authRepo.Register(new User { Email = request.Email }, request.Password);
            if (response.Token == null)
            {
                return BadRequest(response);
            }

            return Created("", response);
        }
    }
}