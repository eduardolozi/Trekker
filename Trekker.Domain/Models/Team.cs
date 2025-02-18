namespace Trekker.Domain.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    
    public List<User> Users { get; set; } = [];
}