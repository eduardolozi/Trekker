using Domain.Models;

namespace Domain.Interfaces;

public interface ITeamRepository
{
    Task InsertTeam(Team team);
    Task<Team?> FindTeam(int id, bool trackChanges);
    Task SaveChangesAsync();
}