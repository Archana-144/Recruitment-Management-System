using Recruitment.Common.DTOs;

namespace Recruitment.Store.Abstraction;

public interface IApplicationStore
{
    Task<List<ApplicationDto>>
        GetApplicationsAsync(
            int pageNumber,
            int pageSize);
    Task<bool>
        CreateApplicationAsync(
            int candidateId,
            CreateApplicationDto dto);

    Task<ApplicationDetailDto?>
        GetApplicationByGuidAsync(
            Guid applicationGuid);

    Task<bool>
        UpdateApplicationAsync(
            Guid applicationGuid,
            UpdateApplicationDto dto);

    Task<bool>
        DeleteApplicationAsync(
            Guid applicationGuid);
}