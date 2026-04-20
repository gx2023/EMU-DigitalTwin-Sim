using EMU.DT.Shared.DTOs;

namespace EMU.DT.AuthService.Services;

public interface IAuthService
{
    Task<BaseResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task<BaseResponse<UserDto>> RegisterAsync(RegisterRequestDto request);
    Task<BaseResponse<bool>> ChangePasswordAsync(ChangePasswordRequestDto request, long userId);
    Task<BaseResponse<bool>> LogoutAsync(long userId);
}
