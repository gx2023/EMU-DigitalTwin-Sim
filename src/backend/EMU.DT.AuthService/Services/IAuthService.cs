using EMU.DT.AuthService.DTOs;

namespace EMU.DT.AuthService.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> ValidateTokenAsync(string token);
        Task<UserInfo> GetUserFromTokenAsync(string token);
    }
}