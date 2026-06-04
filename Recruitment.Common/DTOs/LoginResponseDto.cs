namespace Recruitment.Common.DTOs;

/// <summary>
/// Represents the response returned after
/// successful authentication.
/// Contains the generated JWT token.
/// </summary>
public class LoginResponseDto
{
    public string Token { get; set; }
}