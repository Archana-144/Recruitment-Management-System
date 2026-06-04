using Recruitment.Common.DTOs;
using Recruitment.Common.Models;
using Recruitment.Services.Abstraction;
using Recruitment.Store.Abstraction;

namespace Recruitment.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;

    public UserService(IUserStore userStore)
    {
        _userStore = userStore;
    }

    public async Task<List<UserDto>>
    GetUsersAsync(
        int pageNumber,
        int pageSize,
        string? role)
    {
        try
        {
            return await _userStore
                .GetUsersAsync(
                    pageNumber,
                    pageSize,
                    role);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<User?>
    GetUserByGuidAsync(
        Guid userGuid)
    {
        try
        {
            return await _userStore
                .GetUserByGuidAsync(
                    userGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
    CreateUserAsync(
        CreateUserDto dto)
    {
        try
        {
            return await _userStore
                .CreateUserAsync(dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
    UpdateUserAsync(
        Guid userGuid,
        UpdateUserDto dto)
    {
        try
        {
            return await _userStore
                .UpdateUserAsync(
                    userGuid,
                    dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
    DeleteUserAsync(
        Guid userGuid)
    {
        try
        {
            return await _userStore
                .DeleteUserAsync(
                    userGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
    BulkInsertUsersAsync(
        List<BulkUserDto> users)
    {
        try
        {
            return await _userStore
                .BulkInsertUsersAsync(
                    users);
        }
        catch (Exception)
        {
            throw;
        }
    }
}