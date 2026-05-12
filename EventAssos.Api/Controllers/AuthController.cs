using EventAssos.Api.Dtos.Requests;
using EventAssos.Api.Dtos.Responses;
using EventAssos.Core.Interfaces.Services.Auth;
using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAssos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService,
        IJwtService _jwtService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto _loginRequest)
        {
            try
            {
                User user = await _authService.LoginAsync(_loginRequest.Email, _loginRequest.Password);
                string token = _jwtService.GenerateToken(user);
                return Ok(new LoginResponseDto
                {
                    Token = token
                });
            }

            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }

        [HttpPost("register")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RegisterAsync(RegisterRequestDTO _request)
        {
            try
            {
                User? user = await _authService.RegisterAsync(_request.Email);

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
