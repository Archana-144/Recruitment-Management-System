using Microsoft.AspNetCore.Mvc;
using Recruitment.Common.Constants;
using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;

namespace Recruitment.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]


public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticates user and returns JWT token.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> LoginAsync(
      LoginRequestDto dto)
    {
        var response =
            await _authService.LoginAsync(dto);

        if (response == null)
        {
            return Unauthorized(
                "Invalid Username or Password");
        }

        return Ok(response);
    }
}