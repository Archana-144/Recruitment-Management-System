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
        try
        {
            return await _interviewStore
                .GetInterviewsAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<InterviewDetailDto?>
        GetInterviewByGuidAsync(
            Guid interviewGuid)
    {
        try
        {
            return await _interviewStore
                .GetInterviewByGuidAsync(
                    interviewGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        CreateInterviewAsync(
            CreateInterviewDto dto)
    {
        try
        {
            return await _interviewStore
                .CreateInterviewAsync(
                    dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        UpdateInterviewAsync(
            Guid interviewGuid,
            UpdateInterviewDto dto)
    {
        try
        {
            return await _interviewStore
                .UpdateInterviewAsync(
                    interviewGuid,
                    dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        DeleteInterviewAsync(
            Guid interviewGuid)
    {
        try
        {
            return await _interviewStore
                .DeleteInterviewAsync(
                    interviewGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }
}