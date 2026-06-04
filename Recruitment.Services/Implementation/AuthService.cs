using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;
using Recruitment.Store.Abstraction;

namespace Recruitment.Services.Implementation;

/// <summary>
/// Handles authentication logic.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IAuthStore _authStore;
    private readonly IConfiguration _configuration;

    public AuthService(
        IAuthStore authStore,
        IConfiguration configuration)
    {
        _authStore = authStore;
        _configuration = configuration;
    }

    /// <summary>
    /// Validates user credentials and
    /// generates JWT token.
    /// </summary>
    /// <param name="dto">
    /// Contains username and password.
    /// </param>
    /// <returns>
    /// Returns JWT token if login succeeds;
    /// otherwise returns null.
    /// </returns>
    public async Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto dto)
    {
        try
        {
            var user =
                await _authStore.GetUserByUsernameAsync(
                    dto.Username);

            if (user == null)
                return null;

            bool isValidPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!isValidPassword)
                return null;

            var tokenHandler =
                new JwtSecurityTokenHandler();

            var key =
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!);

            var tokenDescriptor =
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim(
                            "UserId",
                            user.UserId.ToString()),

                        new Claim(
                            ClaimTypes.Name,
                            user.Username),

                        new Claim(
                            ClaimTypes.Role,
                            user.Role),

                        new Claim(
                            "UserGuid",
                            user.UserGuid.ToString()),

                        new Claim(
                            "FullName",
                            user.FullName)
                    ]),

                    Issuer =
                        _configuration["Jwt:Issuer"],

                    Audience =
                        _configuration["Jwt:Audience"],

                    Expires =
                        DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                        new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                };

            var token =
                tokenHandler.CreateToken(
                    tokenDescriptor);

            return new LoginResponseDto
            {
                Token =
                    tokenHandler.WriteToken(token)
            };
        }
        catch (Exception)
        {
            throw;
        }
    }
}