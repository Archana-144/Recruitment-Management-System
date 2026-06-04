using System.ComponentModel.DataAnnotations;

namespace Recruitment.Common.DTOs;

public class UpdateApplicationDto
{
    [Required]
    public string ApplicationStatus { get; set; } =
    string.Empty;


[Required]
    public string Remarks { get; set; } =
    string.Empty;


}
