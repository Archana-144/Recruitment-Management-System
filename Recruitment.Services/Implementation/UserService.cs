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
        return await _userStore
            .GetUsersAsync(
                pageNumber,
                pageSize,
                role);
    }
    public async Task<User?> GetUserByGuidAsync(Guid userGuid)
    {
        return await _userStore.GetUserByGuidAsync(userGuid);
    }

    public async Task<bool> CreateUserAsync(CreateUserDto dto)
    {
        return await _userStore.CreateUserAsync(dto);
    }

    public async Task<bool> UpdateUserAsync(Guid userGuid, UpdateUserDto dto)
    {
        return await _userStore.UpdateUserAsync(userGuid, dto);
    }

    public async Task<bool> DeleteUserAsync(Guid userGuid)
    {
        return await _userStore.DeleteUserAsync(userGuid);
    }
    public async Task<bool>
    BulkInsertUsersAsync(
        List<BulkUserDto> users)
    {
        return await _userStore
            .BulkInsertUsersAsync(
                users);
    }
}