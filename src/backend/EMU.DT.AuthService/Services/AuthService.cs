using EMU.DT.AuthService.Data;
using EMU.DT.Shared.DTOs;
using EMU.DT.Shared.Enums;
using EMU.DT.Shared.Extensions;
using EMU.DT.Shared.Models;
using EMU.DT.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.AuthService.Services;

public class AuthService : IAuthService
{
    private readonly AuthDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(AuthDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<BaseResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null)
        {
            return BaseResponse<LoginResponseDto>.Fail("用户名或密码错误");
        }

        if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            return BaseResponse<LoginResponseDto>.Fail("用户名或密码错误");
        }

        var token = _tokenService.GenerateToken(user);

        var response = new LoginResponseDto
        {
            Token = token,
            User = user.ToDto()
        };

        return BaseResponse<LoginResponseDto>.Success(response);
    }

    public async Task<BaseResponse<UserDto>> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);
        if (existingUser != null)
        {
            return BaseResponse<UserDto>.Fail("用户名或邮箱已存在");
        }

        var passwordHash = PasswordHelper.HashPassword(request.Password);

        var user = new UserInfo
        {
            Id = IdGenerator.NextId(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            RealName = request.RealName,
            PhoneNumber = request.PhoneNumber,
            Role = RoleType.Operator,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return BaseResponse<UserDto>.Success(user.ToDto());
    }

    public async Task<BaseResponse<bool>> ChangePasswordAsync(ChangePasswordRequestDto request, long userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return BaseResponse<bool>.Fail("用户不存在");
        }

        if (!PasswordHelper.VerifyPassword(request.OldPassword, user.PasswordHash))
        {
            return BaseResponse<bool>.Fail("原密码错误");
        }

        user.PasswordHash = PasswordHelper.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return BaseResponse<bool>.Success(true);
    }

    public async Task<BaseResponse<bool>> LogoutAsync(long userId)
    {
        return BaseResponse<bool>.Success(true);
    }
}
