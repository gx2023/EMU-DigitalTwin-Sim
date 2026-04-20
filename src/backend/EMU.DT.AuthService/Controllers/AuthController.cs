using EMU.DT.AuthService.DTOs;
using EMU.DT.AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMU.DT.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("validate")]
        [Authorize]
        public async Task<ActionResult<bool>> ValidateToken()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var isValid = await _authService.ValidateTokenAsync(token);
                return Ok(isValid);
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpGet("user-info")]
        [Authorize]
        public async Task<ActionResult<UserInfo>> GetUserInfo()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userInfo = await _authService.GetUserFromTokenAsync(token);
                if (userInfo == null)
                {
                    return Unauthorized();
                }
                return Ok(userInfo);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}