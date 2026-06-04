using System.ComponentModel.DataAnnotations;

namespace Recruitment.Common.DTOs;

public class CreateApplicationDto
{


    [Required]
    public Guid JobPostingGuid { get; set; }

    [Required]
    public string Remarks { get; set; } =
        string.Empty;


}
