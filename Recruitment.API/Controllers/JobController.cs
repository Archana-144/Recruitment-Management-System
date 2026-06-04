using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Common.Constants;
using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;

namespace Recruitment.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]

/// <summary>
/// Job management APIs.
/// </summary>

public class JobController : ControllerBase
{
    private readonly IJobService _jobService;


public JobController(
    IJobService jobService)
    {
        _jobService = jobService;
    }
    [Authorize(Roles = "Admin,HR,Candidate")]
    [HttpGet]
    public async Task<IActionResult>
    GetJobs(
        int pageNumber = 1,
        int pageSize = 10)
    {
        var jobs =
            await _jobService
                .GetJobPostingsAsync(
                    pageNumber,
                    pageSize);

        return Ok(jobs);
    }
    [Authorize(Roles = "Admin,HR,Candidate")]
    [HttpGet("{guid}")]
    public async Task<IActionResult>
    GetJobByGuid(
        Guid guid)
    {
        var job =
            await _jobService
                .GetJobPostingByGuidAsync(
                    guid);

        if (job == null)
            return NotFound();

        return Ok(job);
    }

    [Authorize(Roles = "Admin,HR")]
    [HttpPost]
    public async Task<IActionResult>
    CreateJob(
        CreateJobPostingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool result =
            await _jobService
                .CreateJobPostingAsync(dto);

        if (!result)
            return BadRequest();

        return Ok(MessageConstants.JobCreated);
    }

    [Authorize(Roles = "Admin,HR")]
    [HttpPut("{guid}")]
    public async Task<IActionResult>
    UpdateJob(
        Guid guid,
        UpdateJobPostingDto dto)
    {
        bool result =
            await _jobService
                .UpdateJobPostingAsync(
                    guid,
                    dto);

        if (!result)
            return BadRequest();

        return Ok(MessageConstants.JobUpdated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{guid}")]
    public async Task<IActionResult>
    DeleteJob(
        Guid guid)
    {
        bool result =
            await _jobService
                .DeleteJobPostingAsync(
                    guid);

        if (!result)
            return BadRequest();

        return Ok(MessageConstants.JobDeleted);
    }


}
