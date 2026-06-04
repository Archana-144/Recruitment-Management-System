using Recruitment.Common.Models;

namespace Recruitment.Store.Abstraction;

/// <summary>
/// Provides authentication related
/// database operations.
/// </summary>
public interface IAuthStore
{
    Task<User?> GetUserByUsernameAsync(string username);
}
