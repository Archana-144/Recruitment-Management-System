using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Common.DTOs;
using Recruitment.Services.Abstraction;

namespace Recruitment.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]


public class InterviewController
    : ControllerBase
{
    private readonly IInterviewService
        _interviewService;

    public InterviewController(
        IInterviewService interviewService)
    {
        _interviewService =
            interviewService;
    }

    /// <summary>
    /// Retrieves all interviews.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Interviewer")]
    [HttpGet]
    public async Task<IActionResult>
        GetInterviewsAsync()
    {
        var interviews =
            await _interviewService
                .GetInterviewsAsync();

        return Ok(interviews);
    }

    /// <summary>
    /// Retrieves interview by Guid.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Interviewer")]
    [HttpGet("{interviewGuid}")]
    public async Task<IActionResult>
        GetInterviewByGuidAsync(
            Guid guid)
    {
        var interview =
            await _interviewService
                .GetInterviewByGuidAsync(
                    guid);

        if (interview == null)
            return NotFound();

        return Ok(interview);
    }

    /// <summary>
    /// Creates a new interview.
    /// </summary>
    [Authorize(Roles = "Admin,HR")]
    [HttpPost("CreateInterviewAsync")]
    public async Task<IActionResult>
        CreateInterviewAsync(
            CreateInterviewDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result =
            await _interviewService
                .CreateInterviewAsync(
                    dto);

        if (!result)
            return BadRequest();

        return Ok(
            "Interview Created Successfully");
    }

    /// <summary>
    /// Updates interview.
    /// </summary>
    [Authorize(Roles = "Admin,HR,Interviewer")]
    [HttpPut("{interviewGuid}")]
    public async Task<IActionResult>
        UpdateInterviewAsync(
            Guid guid,
            UpdateInterviewDto dto)
    {
        bool result =
            await _interviewService
                .UpdateInterviewAsync(
                    guid,
                    dto);

        if (!result)
            return BadRequest();

        return Ok(
            "Interview Updated Successfully");
    }

    /// <summary>
    /// Deletes interview.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{interviewGuid}")]
    public async Task<IActionResult>
        DeleteInterviewAsync(
            Guid guid)
    {
        bool result =
            await _interviewService
                .DeleteInterviewAsync(
                    guid);

        if (!result)
            return BadRequest();

        return Ok(
            "Interview Deleted Successfully");
    }
}