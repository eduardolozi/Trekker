namespace Domain.Models;

public class Workspace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? PhotoPath { get; set; }
    public List<User> Members { get; set; } = [];
    public List<Team> Teams { get; set; } = [];
}