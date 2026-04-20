using EMU.DT.AuthService.Data;
using EMU.DT.Shared.DTOs;
using EMU.DT.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AuthDbContext _context;

    public UsersController(AuthDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<UserDto>>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        var userDtos = users.Select(u => u.ToDto());
        return Ok(BaseResponse<IEnumerable<UserDto>>.Success(userDtos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<UserDto>>> GetUser(long id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound(BaseResponse<UserDto>.Fail("用户不存在"));
        }
        return Ok(BaseResponse<UserDto>.Success(user.ToDto()));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<UserDto>>> UpdateUser(long id, [FromBody] UpdateUserRequestDto request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound(BaseResponse<UserDto>.Fail("用户不存在"));
        }

        user.RealName = request.RealName;
        user.PhoneNumber = request.PhoneNumber;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(BaseResponse<UserDto>.Success(user.ToDto()));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> DeleteUser(long id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound(BaseResponse<bool>.Fail("用户不存在"));
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok(BaseResponse<bool>.Success(true));
    }
}
