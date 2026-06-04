public class UpdateInterviewDto
{
    public Guid InterviewGuid { get; set; }

    public DateTime InterviewDate { get; set; }

    public string Feedback { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;
}