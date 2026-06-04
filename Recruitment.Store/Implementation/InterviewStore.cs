using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Recruitment.Common.DTOs;
using Recruitment.Store.Abstraction;
using System.Data;

namespace Recruitment.Store.Implementation;

public class InterviewStore : IInterviewStore
{
    private readonly IConfiguration _configuration;

    public InterviewStore(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool>
        CreateInterviewAsync(
            CreateInterviewDto dto)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_InsertInterview",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@ApplicationGuid",
    dto.ApplicationGuid
        .ToString("N")
        .ToUpper());

        command.Parameters.AddWithValue(
            "@InterviewerGuid",
            dto.InterviewerGuid
                .ToString("N")
                .ToUpper());

        command.Parameters.AddWithValue(
            "@InterviewDate",
            dto.InterviewDate);

        command.Parameters.AddWithValue(
            "@Feedback",
            dto.Feedback);

        command.Parameters.AddWithValue(
            "@Result",
            dto.Result);

        command.Parameters.AddWithValue(
            "@CreatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool>
        UpdateInterviewAsync(
            Guid interviewGuid,
            UpdateInterviewDto dto)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_UpdateInterview",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@InterviewGuid", interviewGuid.ToString("N").ToUpper());

        command.Parameters.AddWithValue(
            "@Feedback",
            dto.Feedback);

        command.Parameters.AddWithValue(
            "@Result",
            dto.Result);

        command.Parameters.AddWithValue(
            "@UpdatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool>
        DeleteInterviewAsync(
            Guid interviewGuid)
    {
        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_DeleteInterview",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@InterviewGuid",
    interviewGuid
        .ToString("N")
        .ToUpper());

        command.Parameters.AddWithValue(
            "@UpdatedBy",
            "System");

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
    public async Task<List<InterviewDto>>
    GetInterviewsAsync()
    {
        List<InterviewDto> interviews = [];

        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_GetInterviews",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        await connection.OpenAsync();

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {

            interviews.Add(
                new InterviewDto
                {
                    InterviewGuid =
                        Guid.Parse(
                            reader["interview_guid"]
                                .ToString()),

                    InterviewerName =
                        reader["interviewer_name"]
                            .ToString(),

                    InterviewDate =
                        Convert.ToDateTime(
                            reader["interview_date"]),

                    Feedback =
                        reader["feedback"]
                            .ToString(),

                    Result =
                        reader["result"]
                            .ToString()
                });
        }

        return interviews;
    }

    public async Task<InterviewDetailDto?>
    GetInterviewByGuidAsync(
        Guid interviewGuid)
    {

        string connectionString =
            _configuration.GetConnectionString(
                "DefaultConnection");

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(
                "usp_GetInterviewByGuid",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;
        command.Parameters.AddWithValue(
    "@InterviewGuid",
    interviewGuid
        .ToString("N")
        .ToUpper());

        await connection.OpenAsync();

        using SqlDataReader reader =
            await command.ExecuteReaderAsync();
      
        if (await reader.ReadAsync())
        {
            return new InterviewDetailDto
            {
                InterviewGuid =
        Guid.ParseExact(
            reader["interview_guid"].ToString()!,
            "N"),

                ApplicationGuid =
        Guid.ParseExact(
            reader["application_guid"].ToString()!,
            "N"),

                InterviewerGuid =
        Guid.ParseExact(
            reader["interviewer_guid"].ToString()!,
            "N"),

                InterviewDate =
        Convert.ToDateTime(
            reader["interview_date"]),

                Feedback =
        reader["feedback"].ToString()!,

                Result =
        reader["result"].ToString()!
            };
        }

        return null;
    }
}