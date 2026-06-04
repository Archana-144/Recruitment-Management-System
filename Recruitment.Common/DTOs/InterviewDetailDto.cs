public class InterviewDetailDto
{
    public Guid InterviewGuid { get; set; }

    public Guid ApplicationGuid { get; set; }

    public Guid InterviewerGuid { get; set; }

    public DateTime InterviewDate { get; set; }

    public string Feedback { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;
}