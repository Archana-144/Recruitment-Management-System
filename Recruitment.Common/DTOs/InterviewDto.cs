namespace Recruitment.Common.DTOs;

public class InterviewDto
{
    public Guid InterviewGuid { get; set; }

    public string InterviewerName { get; set; } = string.Empty;

    public DateTime InterviewDate { get; set; }

    public string Feedback { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;
}