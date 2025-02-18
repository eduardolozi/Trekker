using System.Text.Json.Serialization;

namespace Trekker.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int KeycloakId { get; set; }
    public string PhotoUrl { get; set; }
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
    
    public List<Permission> ExtraPermissions { get; set; }
    public List<Workspace> Workspaces { get; set; } = [];
    public List<Team> Teams { get; set; } = [];
    public List<Chat> Chats { get; set; } = [];
}