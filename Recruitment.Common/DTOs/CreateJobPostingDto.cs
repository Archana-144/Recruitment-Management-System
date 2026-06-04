namespace Recruitment.Common.DTOs;

public class CreateJobPostingDto
{
    public int JobRoleId { get; set; }

    public string JobTitle { get; set; }

    public int Openings { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}