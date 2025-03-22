using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces;

public interface IWorkspaceService
{
    Task CreateWorkspace(Workspace workspace, int creatorUserId);
    Task<List<Workspace>> GetAllWorkspacesAsync(WorkspaceFilter? filter = null);
    Task<Workspace?> GetWorkspace(int id);
}