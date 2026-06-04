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

    /// <summary>
    /// Retrieves all active job postings.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Candidate")]
    [HttpGet("GetJobsAsync")]
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

    /// <summary>
    /// Retrieves a job posting by Guid.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Candidate")]
    [HttpGet("GetJobByGuidAsync/{jobGuid}")]
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

    /// <summary>
    /// Creates a new job posting.
    /// </summary>

    [Authorize(Roles = "Admin,HR")]
    [HttpPost("CreateJobAsync")]
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


    /// <summary>
    /// Updates an existing job posting.
    /// </summary>
    [Authorize(Roles = "Admin,HR")]
    [HttpPut("UpdateJobAsync/{jobGuid}")]
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


    /// <summary>
    /// Soft deletes a job posting.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteJobAsync/{jobGuid}")]
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
