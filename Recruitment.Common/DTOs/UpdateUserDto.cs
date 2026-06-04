using System.ComponentModel.DataAnnotations;

namespace Recruitment.Common.DTOs;

public class UpdateUserDto
{
    [Required]
    public string FullName { get; set; }

[Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Role { get; set; }


}
