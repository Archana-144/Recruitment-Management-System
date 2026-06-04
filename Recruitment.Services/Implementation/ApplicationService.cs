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
        return await _applicationStore
            .GetApplicationsAsync(
                pageNumber,
                pageSize);
    }
    public async Task<bool>
    CreateApplicationAsync(
        int candidateId,
        CreateApplicationDto dto)
    {
        return await _applicationStore
            .CreateApplicationAsync(
                candidateId,
                dto);
    }

    public async Task<ApplicationDetailDto?>
        GetApplicationByGuidAsync(
            Guid applicationGuid)
    {
        return await _applicationStore
            .GetApplicationByGuidAsync(
                applicationGuid);
    }

    public async Task<bool>
        UpdateApplicationAsync(
            Guid applicationGuid,
            UpdateApplicationDto dto)
    {
        return await _applicationStore
            .UpdateApplicationAsync(
                applicationGuid,
                dto);
    }

    public async Task<bool>
    DeleteApplicationAsync(
        Guid applicationGuid)
    {
        return await _applicationStore
            .DeleteApplicationAsync(
                applicationGuid);
    }
}