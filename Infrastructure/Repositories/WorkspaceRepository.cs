using Domain.DTOs;
using Domain.Filters;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WorkspaceRepository(TrekkerContext db) : IWorkspaceRepository
{
    public async Task InsertWorkspace(Workspace workspace)
    {
        await db.Workspace.AddAsync(workspace);
        await db.SaveChangesAsync();
    }

    public Task<List<Workspace>> FindWorkspaces(WorkspaceFilter? filter = null)
    {
        if (filter == null) return db.Workspace.AsNoTracking().AsQueryable().ToListAsync();
        
        var query = db.Workspace.AsNoTracking().AsQueryable();
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{filter.Name.ToLower()}%"));
        }
        if (filter.UserId.HasValue)
        {
            query = query.Where(x => x.Members.Any(user => user.Id == filter.UserId));
        }
        return query.ToListAsync();
    }

    public Task<Workspace?> FindWorkspace(int id)
    {
        return db.Workspace
            .Include(x => x.Members)
            .Include(x => x.Teams)
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}