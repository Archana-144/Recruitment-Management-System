using System.ComponentModel.DataAnnotations;

namespace Recruitment.Common.DTOs;

public class UpdateJobPostingDto
{
    [Required]
    public int JobRoleId { get; set; }


[Required]
    public string JobTitle { get; set; }

    [Required]
    public int Openings { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }


}
