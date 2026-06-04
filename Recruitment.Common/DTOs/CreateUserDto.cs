using System.ComponentModel.DataAnnotations;

namespace Recruitment.Common.DTOs;

public class CreateUserDto
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

    [Required]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }


}
