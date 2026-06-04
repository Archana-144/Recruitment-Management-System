namespace Recruitment.Common.Models;

public class Interview
{
    public int InterviewId { get; set; }

    public int ApplicationId { get; set; }

    public int InterviewerId { get; set; }

    public DateTime InterviewDate { get; set; }

    public string Feedback { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid InterviewGuid { get; set; }
}