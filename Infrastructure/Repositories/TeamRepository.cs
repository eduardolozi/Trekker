using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TeamRepository(TrekkerContext db) : ITeamRepository
{
    public async Task InsertTeam(Team team)
    {
        await db.Team.AddAsync(team);
        await db.SaveChangesAsync();
    }

    public Task<Team?> FindTeam(int id, bool trackChanges)
    {
        var query = db.Team.AsQueryable();
        if(!trackChanges) query = query.AsNoTracking();
        
        return query
            .Include(x => x.Users)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
    
    public Task SaveChangesAsync() => db.SaveChangesAsync();
}