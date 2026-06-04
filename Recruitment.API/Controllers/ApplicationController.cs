using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;
using System.Security.Claims;
namespace Recruitment.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ApplicationController : ControllerBase
{
    private readonly IApplicationService
        _applicationService;

    public ApplicationController(
        IApplicationService applicationService)
    {
        _applicationService =
            applicationService;
    }

    /// <summary>
    /// Retrieves all applications.
    /// </summary>
     [Authorize(Roles = "Admin,HR,Interviewer")]
    [HttpGet("GetApplicationsAsync")]
    public async Task<IActionResult>
        GetApplications(
            int pageNumber = 1,
            int pageSize = 10)
    {
        var applications =
            await _applicationService
                .GetApplicationsAsync(
                    pageNumber,
                    pageSize);

        return Ok(applications);
    }

    /// <summary>
    /// Creates a new application.
    /// </summary>
    /// 
    [Authorize(Roles = "Candidate")]
    [HttpPost("CreateApplicationAsync")]
    public async Task<IActionResult>
     CreateApplication(
         CreateApplicationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var candidateId =
            Convert.ToInt32(
                User.FindFirst("UserId")?.Value);

        bool result =
            await _applicationService
                .CreateApplicationAsync(
                    candidateId,
                    dto);

        if (!result)
            return BadRequest();

        return Ok(
            "Application Created Successfully");
    }
    /// <summary>
    /// Retrieves an application by Guid.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Interviewer")]
    [HttpGet("GetApplicationByGuidAsync/{guid}")]
    public async Task<IActionResult>
 GetApplicationByGuid(
     Guid guid)
    { 
        var application =
            await _applicationService
                .GetApplicationByGuidAsync(
                    guid);

        if (application == null)
            return NotFound();

        return Ok(application);
    }

    /// <summary>
    /// Updates application status.
    /// </summary>
    [Authorize(Roles = "Admin,HR")]
    [HttpPut("UpdateApplicationAsync/{guid}")]
    public async Task<IActionResult>
UpdateApplication(
    Guid guid,
    UpdateApplicationDto dto)
    {
        bool result =
            await _applicationService
                .UpdateApplicationAsync(
                    guid,
                    dto);

        if (!result)
            return BadRequest();

        return Ok(
            "Application Updated Successfully");
    }

    /// <summary>
    /// Soft deletes an application.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteApplicationAsync/{Guid}")]
    public async Task<IActionResult>
        DeleteApplication(
            Guid guid)
    {
        bool result =
            await _applicationService
                .DeleteApplicationAsync(
                    guid);

        if (!result)
            return BadRequest();

        return Ok(
            "Application Deleted Successfully");
    }
}