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
        return await _jobStore
            .GetJobPostingsAsync(
                pageNumber,
                pageSize);
    }

    public async Task<JobPostingDto?>
        GetJobPostingByGuidAsync(
            Guid jobPostingGuid)
    {
        return await _jobStore
            .GetJobPostingByGuidAsync(
                jobPostingGuid);
    }

    public async Task<bool>
        CreateJobPostingAsync(
            CreateJobPostingDto dto)
    {
        return await _jobStore
            .CreateJobPostingAsync(dto);
    }

    public async Task<bool>
        UpdateJobPostingAsync(
            Guid jobPostingGuid,
            UpdateJobPostingDto dto)
    {
        return await _jobStore
            .UpdateJobPostingAsync(
                jobPostingGuid,
                dto);
    }

    public async Task<bool>
        DeleteJobPostingAsync(
            Guid jobPostingGuid)
    {
        return await _jobStore
            .DeleteJobPostingAsync(
                jobPostingGuid);
    }


}
