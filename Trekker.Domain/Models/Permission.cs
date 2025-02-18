namespace Trekker.Domain.Models;

public class Permission
{
    public int Id { get; set; }
    public string Description { get; set; }
    
    public List<User> Users { get; set; } = [];
    public List<Role> Roles { get; set; } = [];
}