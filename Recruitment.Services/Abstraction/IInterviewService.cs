using Recruitment.Common.DTOs;

namespace Recruitment.Services.Abstraction;

public interface IInterviewService
{
    Task<List<InterviewDto>>
        GetInterviewsAsync();

    Task<InterviewDetailDto?>
        GetInterviewByGuidAsync(
            Guid interviewGuid);

    Task<bool>
        CreateInterviewAsync(
            CreateInterviewDto dto);

    Task<bool>
        UpdateInterviewAsync(
            Guid interviewGuid,
            UpdateInterviewDto dto);

    Task<bool>
        DeleteInterviewAsync(
            Guid interviewGuid);
}