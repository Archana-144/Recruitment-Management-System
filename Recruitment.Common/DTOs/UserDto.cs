namespace Recruitment.Common.DTOs;

public class UserDto
{
    public int TotalRecords { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid UserGuid { get; set; }
}