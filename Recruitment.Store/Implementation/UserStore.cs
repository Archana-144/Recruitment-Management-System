using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Recruitment.Common.Models;
using Recruitment.Store.Abstraction;
using System.Data;
using Recruitment.Common.DTOs;
namespace Recruitment.Store.Implementation;



public class UserStore : IUserStore
{
    private readonly IConfiguration _configuration;

    public UserStore(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    /// <summary>
    /// Retrieves all active users from the database.
    /// Executes the usp_GetUsers stored procedure and
    /// maps the result set to a list of User objects.
    /// </summary>
    /// <returns>
    /// Returns a collection of active users.
    /// </returns>
    public async Task<List<UserDto>>
    GetUsersAsync(
        int pageNumber,
        int pageSize,
        string? role)
    {
        List<UserDto> users = new();

        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_GetUsers",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@PageNumber",
            pageNumber);

        command.Parameters.AddWithValue(
            "@PageSize",
            pageSize);
        command.Parameters.AddWithValue(
    "@Role",
    string.IsNullOrEmpty(role)
        ? DBNull.Value
        : role);

        await connection.OpenAsync();

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            users.Add(
                new UserDto
                {
                    TotalRecords =
                        Convert.ToInt32(
                            reader["total_records"]),

                    UserGuid =
                        Guid.Parse(
                            reader["user_guid"]
                                .ToString()),

                    FullName =
                        reader["full_name"]
                            .ToString(),

                    Email =
                        reader["email"]
                            .ToString(),

                    Phone =
                        reader["phone"]
                            .ToString(),

                    Role =
                        reader["role"]
                            .ToString(),

                    Username =
                        reader["username"]
                            .ToString(),

                    IsActive =
                        Convert.ToBoolean(
                            reader["is_active"])
                });
        }

        return users;
    }
    /// <summary>
    /// Retrieves a specific user based on UserGuid.
    /// Executes the usp_GetUserByGuid stored procedure.
    /// </summary>
    /// <param name="userGuid">
    /// Unique identifier of the user.
    /// </param>
    /// <returns>
    /// Returns user details if found;
    /// otherwise returns null.
    /// </returns>
    public async Task<User?> GetUserByGuidAsync(Guid userGuid)
    {
        string connectionString =
            _configuration.GetConnectionString("DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand("usp_GetUserByGuid", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
    "@UserGuid",
    userGuid
        .ToString("N")
        .ToUpper());
        await connection.OpenAsync();

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new User
            {
                UserGuid = Guid.Parse(reader["user_guid"].ToString()),
                FullName = reader["full_name"].ToString(),
                Email = reader["email"].ToString(),
                Phone = reader["phone"].ToString(),
                Role = reader["role"].ToString(),
                Username = reader["username"].ToString(),
                IsActive = Convert.ToBoolean(reader["is_active"])
            };
        }

        return null;
    }


    /// <summary>
    /// Creates a new user in the system.
    /// Executes the usp_InsertUser stored procedure.
    /// Password is encrypted using BCrypt before
    /// storing it in the database.
    /// </summary>
    /// <param name="dto">
    /// Contains user details required for creation.
    /// </param>
    /// <returns>
    /// Returns true if user creation is successful;
    /// otherwise returns false.
    /// </returns>
    public async Task<bool> CreateUserAsync(CreateUserDto dto)
    {
        string connectionString =
            _configuration.GetConnectionString("DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand("usp_InsertUser", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@FullName", dto.FullName);
        command.Parameters.AddWithValue("@Email", dto.Email);
        command.Parameters.AddWithValue("@Phone", dto.Phone);
        command.Parameters.AddWithValue("@Role", dto.Role);
        command.Parameters.AddWithValue("@Username", dto.Username);


        command.Parameters.AddWithValue(
            "@PasswordHash",
            BCrypt.Net.BCrypt.HashPassword(dto.Password));

        command.Parameters.AddWithValue(
            "@CreatedBy",
            "System");

        await connection.OpenAsync();


            await command.ExecuteNonQueryAsync();

        return true;
    }

    /// <summary>
    /// Updates an existing user's information.
    /// Executes the usp_UpdateUser stored procedure.
    /// </summary>
    /// <param name="userGuid">
    /// Unique identifier of the user.
    /// </param>
    /// <param name="dto">
    /// Contains updated user information.
    /// </param>
    /// <returns>
    /// Returns true if update is successful;
    /// otherwise returns false.
    /// </returns>
    public async Task<bool> UpdateUserAsync(
        Guid userGuid,
        UpdateUserDto dto)
    {
        string connectionString =
            _configuration.GetConnectionString("DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand("usp_UpdateUser", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
    "@UserGuid",
    userGuid
        .ToString("N")
        .ToUpper());
        command.Parameters.AddWithValue("@FullName", dto.FullName);
        command.Parameters.AddWithValue("@Email", dto.Email);
        command.Parameters.AddWithValue("@Phone", dto.Phone);
        command.Parameters.AddWithValue("@Role", dto.Role);
        command.Parameters.AddWithValue("@UpdatedBy", "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    /// <summary>
    /// Soft deletes a user from the system.
    /// Executes the usp_DeleteUser stored procedure.
    /// The user record is not physically deleted;
    /// instead IsActive is set to false.
    /// </summary>
    /// <param name="userGuid">
    /// Unique identifier of the user.
    /// </param>
    /// <returns>
    /// Returns true if deletion is successful;
    /// otherwise returns false.
    /// </returns>
    public async Task<bool> DeleteUserAsync(Guid userGuid)
    {
        string connectionString =
            _configuration.GetConnectionString("DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand("usp_DeleteUser", connection);

        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@UserGuid",
    userGuid
        .ToString("N")
        .ToUpper());
        command.Parameters.AddWithValue("@UpdatedBy", "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
    public async Task<bool>
    BulkInsertUsersAsync(
        List<BulkUserDto> users)
    {
        DataTable table = new();

        table.Columns.Add("FullName");
        table.Columns.Add("Email");
        table.Columns.Add("Phone");
        table.Columns.Add("Role");
        table.Columns.Add("Username");
        table.Columns.Add("PasswordHash");
        table.Columns.Add("CreatedBy");

        foreach (var user in users)
        {
            table.Rows.Add(
                user.FullName,
                user.Email,
                user.Phone,
                user.Role,
                user.Username,
                user.PasswordHash,
                user.CreatedBy);
        }

        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new(connectionString);

        using SqlCommand command =
            new("usp_BulkInsertUsers",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        SqlParameter parameter =
            command.Parameters.AddWithValue(
                "@Users",
                table);

        parameter.SqlDbType =
            SqlDbType.Structured;

        parameter.TypeName =
            "UserBulkInsert_Type";

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

}
