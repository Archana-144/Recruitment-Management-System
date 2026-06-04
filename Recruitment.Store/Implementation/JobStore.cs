using Microsoft.Extensions.Configuration;
using Recruitment.Store.Abstraction;

namespace Recruitment.Store.Implementation;

using Microsoft.Data.SqlClient;
using Recruitment.Common.DTOs;
using System.Data;


public class JobStore : IJobStore
{
    private readonly IConfiguration _configuration;

    public JobStore(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<List<JobPostingDto>>
    GetJobPostingsAsync(
        int pageNumber,
        int pageSize)
    {
        List<JobPostingDto> jobs = [];

        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_GetJobPostings",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@PageNumber",
            pageNumber);

        command.Parameters.AddWithValue(
            "@PageSize",
            pageSize);

        await connection.OpenAsync();

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            jobs.Add(
                new JobPostingDto
                {
                    JobPostingGuid =
                        Guid.Parse(
                            reader["job_posting_guid"]
                                .ToString()),

                    JobTitle =
                        reader["job_title"]
                            .ToString(),

                    RoleName =
                        reader["role_name"]
                            .ToString(),

                    Openings =
                        Convert.ToInt32(
                            reader["openings"]),

                    StartDate =
                        Convert.ToDateTime(
                            reader["start_date"]),

                    EndDate =
                        Convert.ToDateTime(
                            reader["end_date"]),

                    IsActive =
                        Convert.ToBoolean(
                            reader["is_active"])
                });
        }

        return jobs;
    }

    public async Task<bool> CreateJobPostingAsync(
    CreateJobPostingDto dto)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_InsertJobPosting",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@JobRoleId",
            dto.JobRoleId);

        command.Parameters.AddWithValue(
            "@JobTitle",
            dto.JobTitle);

        command.Parameters.AddWithValue(
            "@Openings",
            dto.Openings);

        command.Parameters.AddWithValue(
            "@Description",
            dto.Description);

        command.Parameters.AddWithValue(
            "@StartDate",
            dto.StartDate);

        command.Parameters.AddWithValue(
            "@EndDate",
            dto.EndDate);

        command.Parameters.AddWithValue(
            "@CreatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<JobPostingDto?>
    GetJobPostingByGuidAsync(
        Guid jobPostingGuid)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_GetJobPostingByGuid",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@JobPostingGuid",
    jobPostingGuid.ToString("N").ToUpper());

        await connection.OpenAsync();

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new JobPostingDto
            {
                JobPostingGuid =
                    Guid.Parse(
                        reader["job_posting_guid"]
                            .ToString()),

                JobTitle =
                    reader["job_title"]
                        .ToString(),

                Openings =
                    Convert.ToInt32(
                        reader["openings"]),

                StartDate =
                    Convert.ToDateTime(
                        reader["start_date"]),

                EndDate =
                    Convert.ToDateTime(
                        reader["end_date"]),

                IsActive =
                    Convert.ToBoolean(
                        reader["is_active"]),

                RoleName = ""
            };
        }

        return null;
    }
    public async Task<bool>
UpdateJobPostingAsync(
Guid jobPostingGuid,
UpdateJobPostingDto dto)
    {
        string connectionString =
        _configuration.GetConnectionString(
        "DefaultConnection");


using SqlConnection connection =
    new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_UpdateJobPosting",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
      "@JobPostingGuid",
      jobPostingGuid.ToString("N").ToUpper());

        command.Parameters.AddWithValue(
            "@JobRoleId",
            dto.JobRoleId);

        command.Parameters.AddWithValue(
            "@JobTitle",
            dto.JobTitle);

        command.Parameters.AddWithValue(
            "@Openings",
            dto.Openings);

        command.Parameters.AddWithValue(
            "@Description",
            dto.Description);

        command.Parameters.AddWithValue(
            "@StartDate",
            dto.StartDate);

        command.Parameters.AddWithValue(
            "@EndDate",
            dto.EndDate);

        command.Parameters.AddWithValue(
            "@UpdatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;


}

    public async Task<bool>
    DeleteJobPostingAsync(
    Guid jobPostingGuid)
    {
        string connectionString =
        _configuration.GetConnectionString(
        "DefaultConnection");


using SqlConnection connection =
    new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_DeleteJobPosting",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
    "@JobPostingGuid",
    jobPostingGuid.ToString("N").ToUpper());

        command.Parameters.AddWithValue(
            "@UpdatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;


}


}
