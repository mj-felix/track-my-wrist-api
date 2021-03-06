using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyWristAPI.Dtos.User;
using TrackMyWristAPI.Models;
using TrackMyWristAPI.Repositories;

namespace TrackMyWristAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponse>> Register(RegisterUserDto request)
        {
            var response = await _authRepository.Register(new User { Email = request.Email }, request.Password);
            if (response.Token == null)
            {
                return BadRequest(response);
            }

            return Created("", response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto request)
        {
            var response = await _authRepository.Login(request.Email, request.Password);
            if (response.Token == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}