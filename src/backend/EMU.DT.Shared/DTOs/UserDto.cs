
using EMU.DT.Shared.Enums;

namespace EMU.DT.Shared.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? RealName { get; set; }
    public string? PhoneNumber { get; set; }
    public RoleType Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class LoginRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
}

public class RegisterRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? RealName { get; set; }
    public string? PhoneNumber { get; set; }
}

public class ChangePasswordRequestDto
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

public class UpdateUserRequestDto
{
    public string? RealName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
