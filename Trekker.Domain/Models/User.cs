namespace Trekker.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string KeycloakId { get; set; }
    public string PhotoUrl { get; set; }
    
    public List<Workspace> Workspaces { get; set; } = [];
    public List<Team> Teams { get; set; } = [];
    public List<Chat> Chats { get; set; } = [];
}