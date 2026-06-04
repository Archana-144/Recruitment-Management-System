namespace Recruitment.Common.DTOs;

public class ApplicationDetailDto
{
    public Guid ApplicationGuid { get; set; }

    public int CandidateId { get; set; }

    public int JobPostingId { get; set; }

    public string ApplicationStatus { get; set; } =
        string.Empty;

    public string Remarks { get; set; } =
        string.Empty;

    public DateTime AppliedDate { get; set; }
}