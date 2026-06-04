using Recruitment.Common.DTOs;

namespace Recruitment.Services.Abstraction;

public interface IJobService
{
    Task<List<JobPostingDto>> GetJobPostingsAsync(
    int pageNumber,
    int pageSize);


Task<JobPostingDto?> GetJobPostingByGuidAsync(
    Guid jobPostingGuid);

    Task<bool> CreateJobPostingAsync(
        CreateJobPostingDto dto);

    Task<bool> UpdateJobPostingAsync(
        Guid jobPostingGuid,
        UpdateJobPostingDto dto);

    Task<bool> DeleteJobPostingAsync(
        Guid jobPostingGuid);


}
