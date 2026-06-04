using Recruitment.Common.DTOs;
using Recruitment.Common.Models;

namespace Recruitment.Store.Abstraction;

public interface IUserStore
{
    Task<List<UserDto>>
    GetUsersAsync(
        int pageNumber,
        int pageSize,
        string? role);
    Task<User?> GetUserByGuidAsync(Guid userGuid);

    Task<bool> CreateUserAsync(CreateUserDto dto);

    Task<bool> UpdateUserAsync(Guid userGuid, UpdateUserDto dto);

    Task<bool> DeleteUserAsync(Guid userGuid);
    Task<bool> BulkInsertUsersAsync(
    List<BulkUserDto> users);
}