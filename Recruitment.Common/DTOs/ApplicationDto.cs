namespace Recruitment.Common.DTOs;

public class ApplicationDto
{
    public Guid ApplicationGuid { get; set; }

    public string CandidateName { get; set; } =
        string.Empty;

    public string JobTitle { get; set; } =
        string.Empty;

    public string ApplicationStatus { get; set; } =
        string.Empty;

    public DateTime AppliedDate { get; set; }
}