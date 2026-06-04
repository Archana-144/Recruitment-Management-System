using Recruitment.Common.DTOs;

namespace Recruitment.Services.Abstraction;

/// <summary>
/// Provides authentication related
/// business operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Validates user credentials and
    /// generates JWT token.
    /// </summary>
    Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto dto);
}