namespace Recruitment.Common.Models;

public class User
{
    public int UserId { get; set; }
    public Guid UserGuid { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Role { get; set; }

    public string Username { get; set; }

    public bool IsActive { get; set; }
    public string PasswordHash { get; set; }
}