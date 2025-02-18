namespace Trekker.Domain.Models;

public class Workspace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    
    public List<User> Users { get; set; } = [];
    public List<Team> Teams { get; set; } = [];
    public List<Chat> Chats { get; set; } = [];
}