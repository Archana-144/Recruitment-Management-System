using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Recruitment.Common.DTOs;
using Recruitment.Store.Abstraction;
using System.Data;

namespace Recruitment.Store.Implementation;

public class ApplicationStore : IApplicationStore
{
    private readonly IConfiguration _configuration;

    public ApplicationStore(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<ApplicationDto>>
        GetApplicationsAsync(
            int pageNumber,
            int pageSize)
    {
        List<ApplicationDto> applications = [];

        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_GetApplications",
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
            applications.Add(
                new ApplicationDto
                {
                    ApplicationGuid =
                        Guid.Parse(
                            reader["application_guid"]
                                .ToString()),

                    CandidateName =
                        reader["candidate_name"]
                            .ToString(),

                    JobTitle =
                        reader["job_title"]
                            .ToString(),

                    ApplicationStatus =
                        reader["application_status"]
                            .ToString(),

                    AppliedDate =
                        Convert.ToDateTime(
                            reader["applied_date"])
                });
        }

        return applications;
    }
    public async Task<bool>
        CreateApplicationAsync(
            int candidateId,
            CreateApplicationDto dto)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_InsertApplication",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
                    "@CandidateId",
                  candidateId);

        command.Parameters.AddWithValue(
     "@JobPostingGuid",
     dto.JobPostingGuid
         .ToString("N")
         .ToUpper());

        command.Parameters.AddWithValue(
            "@Remarks",
            dto.Remarks);

        command.Parameters.AddWithValue(
            "@CreatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<ApplicationDetailDto?>
        GetApplicationByGuidAsync(
            Guid applicationGuid)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_GetApplicationByGuid",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@ApplicationGuid",
    applicationGuid
        .ToString("N")
        .ToUpper());
        await connection.OpenAsync();

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new ApplicationDetailDto
            {
                ApplicationGuid =
                    Guid.Parse(
                        reader["application_guid"]
                            .ToString()),

                CandidateId =
                    Convert.ToInt32(
                        reader["candidate_id"]),

                JobPostingId =
                    Convert.ToInt32(
                        reader["job_posting_id"]),

                ApplicationStatus =
                    reader["application_status"]
                        .ToString(),

                Remarks =
                    reader["remarks"]
                        .ToString(),

                AppliedDate =
                    Convert.ToDateTime(
                        reader["applied_date"])
            };
        }

        return null;
    }

    public async Task<bool>
        UpdateApplicationAsync(
            Guid applicationGuid,
            UpdateApplicationDto dto)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_UpdateApplication",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@ApplicationGuid",
    applicationGuid
        .ToString("N")
        .ToUpper());

        command.Parameters.AddWithValue(
            "@ApplicationStatus",
            dto.ApplicationStatus);

        command.Parameters.AddWithValue(
            "@Remarks",
            dto.Remarks);

        command.Parameters.AddWithValue(
            "@UpdatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
    public async Task<bool>
    DeleteApplicationAsync(
        Guid applicationGuid)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_DeleteApplication",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@ApplicationGuid",
    applicationGuid
        .ToString("N")
        .ToUpper());

        command.Parameters.AddWithValue(
            "@UpdatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}