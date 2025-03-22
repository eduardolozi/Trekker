using Domain.DTOs;
using Domain.Filters;
using Domain.Models;

namespace Domain.Interfaces;

public interface IWorkspaceRepository
{
    Task InsertWorkspace(Workspace workspace);
    Task<List<Workspace>> FindWorkspaces(WorkspaceFilter? filter = null);
    Task<Workspace?> FindWorkspace(int id);
}