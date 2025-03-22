using System.Text.Json.Serialization;

namespace Domain.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; } = [];
    public int WorkspaceId { get; set; }
    [JsonIgnore]  Workspace Workspace { get; set; }
    public string? PhotoPath { get; set; }
}