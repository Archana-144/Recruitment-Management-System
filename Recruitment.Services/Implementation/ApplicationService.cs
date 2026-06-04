using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;
using Recruitment.Store.Abstraction;

namespace Recruitment.Services.Implementation;

public class ApplicationService
    : IApplicationService
{
    private readonly IApplicationStore
        _applicationStore;

    public ApplicationService(
        IApplicationStore applicationStore)
    {
        _applicationStore =
            applicationStore;
    }

    public async Task<List<ApplicationDto>>
        GetApplicationsAsync(
            int pageNumber,
            int pageSize)
    {
        try
        {
            return await _applicationStore
                .GetApplicationsAsync(
                    pageNumber,
                    pageSize);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        CreateApplicationAsync(
            int candidateId,
            CreateApplicationDto dto)
    {
        try
        {
            return await _applicationStore
                .CreateApplicationAsync(
                    candidateId,
                    dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApplicationDetailDto?>
        GetApplicationByGuidAsync(
            Guid applicationGuid)
    {
        try
        {
            return await _applicationStore
                .GetApplicationByGuidAsync(
                    applicationGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        UpdateApplicationAsync(
            Guid applicationGuid,
            UpdateApplicationDto dto)
    {
        try
        {
            return await _applicationStore
                .UpdateApplicationAsync(
                    applicationGuid,
                    dto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool>
        DeleteApplicationAsync(
            Guid applicationGuid)
    {
        try
        {
            return await _applicationStore
                .DeleteApplicationAsync(
                    applicationGuid);
        }
        catch (Exception)
        {
            throw;
        }
    }
}