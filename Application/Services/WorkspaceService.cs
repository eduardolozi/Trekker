using Application.Interfaces;
using Domain.DTOs;
using Domain.Filters;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class WorkspaceService(IWorkspaceRepository workspaceRepository, IUserService userService) : IWorkspaceService
{
    public async Task CreateWorkspace(Workspace workspace, int creatorUserId)
    {
        var user = await userService.GetUser(creatorUserId, new UserFilter {IncludeWorkspaces = false})
            ?? throw new Exception("The user could not be found");
        
        workspace.Members = [user];
        await workspaceRepository.InsertWorkspace(workspace);
    }

    public Task<List<Workspace>> GetAllWorkspacesAsync(WorkspaceFilter? filter = null)
    {
        return workspaceRepository.FindWorkspaces(filter);
    }

    public Task<Workspace?> GetWorkspace(int id)
    {
        return workspaceRepository.FindWorkspace(id);
    }
}