namespace Trekker.Domain.Models;

public class Chat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    
    public List<User> Users { get; set; } = [];
    public List<ChatMessage> Messages { get; set; } = [];
    public List<ChatActivity> Activities { get; set; } = [];
}