using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;
using Recruitment.Store.Abstraction;

namespace Recruitment.Services.Implementation;

public class JobService : IJobService
{
    private readonly IJobStore _jobStore;

    public JobService(
        IJobStore jobStore)
    {
        _jobStore = jobStore;
    }

    public async Task<List<JobPostingDto>>
        GetJobPostingsAsync(
            int pageNumber,
            int pageSize)
    {
        try
        {
            return await _jobStore
                .GetJobPostingsAsync(
                    pageNumber,
                    pageSize);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JobPostingDto?>
        GetJobPostingByGuidAsync(
            Guid jobPostingGuid)
    {
        try
        {
            return await _jobStore
                .GetJobPostingByGuidAsync(
                    jobPostingGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        CreateJobPostingAsync(
            CreateJobPostingDto dto)
    {
        try
        {
            return await _jobStore
                .CreateJobPostingAsync(dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        UpdateJobPostingAsync(
            Guid jobPostingGuid,
            UpdateJobPostingDto dto)
    {
        try
        {
            return await _jobStore
                .UpdateJobPostingAsync(
                    jobPostingGuid,
                    dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        DeleteJobPostingAsync(
            Guid jobPostingGuid)
    {
        try
        {
            return await _jobStore
                .DeleteJobPostingAsync(
                    jobPostingGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }
}