using EMU.DT.Shared.Models;

namespace EMU.DT.AuthService.Services;

public interface ITokenService
{
    string GenerateToken(UserInfo user);
    Task<bool> ValidateTokenAsync(string token);
}
