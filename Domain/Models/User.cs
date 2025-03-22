using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public UserRoleEnum Role { get; set; }
    [JsonIgnore] public string KeycloakId { get; set; } = string.Empty;
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped] public string? Password { get; set; }
    public string Email { get; set; }
    public string? PhotoPath  { get; set; }
    public List<Workspace> Workspaces { get; set; } = [];
    public List<Chat> Chats { get; set; } = [];
}