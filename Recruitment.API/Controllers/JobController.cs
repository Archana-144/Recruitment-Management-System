using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Common.Constants;
using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;

namespace Recruitment.API.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
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

    /// <summary>
    /// Retrieves all active job postings.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Candidate")]
    [HttpGet]
    public async Task<IActionResult>
    GetJobsAsync(
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

    /// <summary>
    /// Retrieves a job posting by Guid.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Candidate")]
    [HttpGet("{jobGuid}")]
    public async Task<IActionResult>
    GetJobByGuidAsync(
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

    /// <summary>
    /// Creates a new job posting.
    /// </summary>

    [Authorize(Roles = "Admin,HR")]
    [HttpPost]
    public async Task<IActionResult>
    CreateJobAsync(
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


    /// <summary>
    /// Updates an existing job posting.
    /// </summary>
    [Authorize(Roles = "Admin,HR")]
    [HttpPut("{jobGuid}")]
    public async Task<IActionResult>
    UpdateJobAsync(
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


    /// <summary>
    /// Soft deletes a job posting.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{jobGuid}")]
    public async Task<IActionResult>
    DeleteJobAsync(
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
