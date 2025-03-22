using Domain.Enums;

namespace Domain.Filters;

public class UserFilter
{
    public string? Username { get; set; }
    public int? TeamId { get; set; }
    public int? WorkspaceId { get; set; }
    public string? Email { get; set; }
    public UserRoleEnum? UserRole { get; set; } 
    
    public bool IncludeWorkspaces { get; set; }
}