namespace Recruitment.Common.Models;

public class JobPosting
{
    public Guid JobPostingGuid { get; set; }

    public string JobTitle { get; set; }

    public int JobRoleId { get; set; }

    public int Openings { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }
}