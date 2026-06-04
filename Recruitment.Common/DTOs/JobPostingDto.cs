namespace Recruitment.Common.DTOs;

public class JobPostingDto
{
    public Guid JobPostingGuid { get; set; }

    public string JobTitle { get; set; } =
        string.Empty;

    public string RoleName { get; set; } =
        string.Empty;

    public int Openings { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }
}