using Recruitment.Common.DTOs;

namespace Recruitment.Store.Abstraction;

public interface IInterviewStore
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