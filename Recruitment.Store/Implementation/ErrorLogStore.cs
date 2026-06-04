using Microsoft.Data.SqlClient;
using Recruitment.Common.Models;
using Recruitment.Store.Abstraction;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Recruitment.Store.Implementation
{
    public class ErrorLogStore : IErrorLogStore
    {
        private readonly string _connectionString;

        public ErrorLogStore(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        public async Task InsertErrorLogAsync(ErrorLog errorLog)
        {
            using SqlConnection con =
                new SqlConnection(_connectionString);

            using SqlCommand cmd =
                new SqlCommand("usp_InsertErrorLog", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@ErrorMessage",
                errorLog.ErrorMessage);

            cmd.Parameters.AddWithValue(
                "@StackTrace",
                errorLog.StackTrace ?? "");

            cmd.Parameters.AddWithValue(
                "@ControllerName",
                errorLog.ControllerName ?? "");

            cmd.Parameters.AddWithValue(
                "@MethodName",
                errorLog.MethodName ?? "");

            await con.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }
    }
}