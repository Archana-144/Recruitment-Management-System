using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;
using Recruitment.Store.Abstraction;

namespace Recruitment.Services.Implementation;

public class InterviewService
    : IInterviewService
{
    private readonly IInterviewStore
        _interviewStore;

    public InterviewService(
        IInterviewStore interviewStore)
    {
        _interviewStore =
            interviewStore;
    }

    public async Task<List<InterviewDto>>
        GetInterviewsAsync()
    {
        return await _interviewStore
            .GetInterviewsAsync();
    }

    public async Task<InterviewDetailDto?>
        GetInterviewByGuidAsync(
            Guid interviewGuid)
    {
        return await _interviewStore
            .GetInterviewByGuidAsync(
                interviewGuid);
    }

    public async Task<bool>
        CreateInterviewAsync(
            CreateInterviewDto dto)
    {
        return await _interviewStore
            .CreateInterviewAsync(
                dto);
    }

    public async Task<bool>
        UpdateInterviewAsync(
            Guid interviewGuid,
            UpdateInterviewDto dto)
    {
        return await _interviewStore
            .UpdateInterviewAsync(
                interviewGuid,
                dto);
    }

    public async Task<bool>
        DeleteInterviewAsync(
            Guid interviewGuid)
    {
        return await _interviewStore
            .DeleteInterviewAsync(
                interviewGuid);
    }
}