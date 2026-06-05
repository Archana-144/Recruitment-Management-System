using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Common.Constants;
using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;

namespace Recruitment.API.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]

[ApiController]

/// <summary>
/// User management APIs.
/// </summary>

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes UserController.
    /// </summary>
   
public UserController(
    IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves all active users.
    /// </summary>
    [Authorize(Roles = "Admin,HR")]
    [HttpGet]
    public async Task<IActionResult>
    GetUsersAsync(
    int pageNumber = 1,
    int pageSize = 10,
    string? role = null)
    {
        var users =
           await _userService.GetUsersAsync(
    pageNumber,
    pageSize,
    role);

        return Ok(users);
    }
    /// <summary>
    /// Retrieves a user based on UserGuid.
    /// </summary>
    [Authorize(Roles = "Admin,HR")]
    [HttpGet("{userGuid}")]
    public async Task<IActionResult>
    GetUserByGuidAsync(Guid guid)
    {
        var user =
            await _userService
                .GetUserByGuidAsync(guid);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult>
    CreateUserAsync(CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool result =
            await _userService
                .CreateUserAsync(dto);

        if (!result)
            return BadRequest();

        return Ok(
            MessageConstants.UserCreated);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{userGuid}")]
    public async Task<IActionResult>
    UpdateUserAsync(
        Guid guid,
        UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool result =
            await _userService
                .UpdateUserAsync(
                    guid,
                    dto);

        if (!result)
            return BadRequest();

        return Ok(
            MessageConstants.UserUpdated);
    }

    /// <summary>
    /// Soft deletes a user.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{userGuid}")]
    public async Task<IActionResult>
    DeleteUserAsync(Guid guid)
    {
        bool result =
            await _userService
                .DeleteUserAsync(guid);

        if (!result)
            return BadRequest();

        return Ok(
            MessageConstants.UserDeleted);
    }

    /// <summary>
    /// Bulk uploads users.
    /// </summary>
   
    
    [Authorize(Roles = "Admin")]
   
    [HttpPost("bulk-upload")]
    public async Task<IActionResult>
    BulkUploadUsersAsync(
        List<BulkUserDto> users)
    {
        bool result =
            await _userService
                .BulkInsertUsersAsync(
                    users);

        if (!result)
            return BadRequest();

        return Ok(
            "Bulk Users Inserted Successfully");
    }


}
