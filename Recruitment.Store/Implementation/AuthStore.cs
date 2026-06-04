using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Recruitment.Common.Models;
using Recruitment.Store.Abstraction;
using System.Data;

namespace Recruitment.Store.Implementation;

/// <summary>
/// Provides authentication related
/// database operations.
/// </summary>
public class AuthStore : IAuthStore
{
    private readonly IConfiguration _configuration;

    public AuthStore(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Retrieves user details based on username.
    /// Executes usp_GetUserByUsername.
    /// </summary>
    /// <summary>
    /// Retrieves user details based on username.
    /// Executes usp_GetUserByUsername.
    /// </summary>
    public async Task<User?> GetUserByUsernameAsync(
        string username)
    {
        try
        {
            string connectionString =
                _configuration.GetConnectionString(
                    "DefaultConnection");

            using SqlConnection connection =
                new SqlConnection(connectionString);

            using SqlCommand command =
                new SqlCommand(
                    "usp_GetUserByUsername",
                    connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@Username",
                username);

            await connection.OpenAsync();

            using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId =
                        Convert.ToInt32(
                            reader["user_id"]),

                    UserGuid =
                        Guid.ParseExact(
                            reader["user_guid"].ToString()!,
                            "N"),

                    FullName =
                        reader["full_name"].ToString()!,

                    Username =
                        reader["username"].ToString()!,

                    PasswordHash =
                        reader["password_hash"].ToString()!,

                    Role =
                        reader["role"].ToString()!,

                    IsActive =
                        Convert.ToBoolean(
                            reader["is_active"])
                };
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
}