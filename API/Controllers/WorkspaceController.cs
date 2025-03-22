using Application.Interfaces;
using Application.Services;
using Domain.Filters;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkspaceController(IWorkspaceService workspaceService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] Workspace workspace, [FromQuery] int userId)
    {
        await workspaceService.CreateWorkspace(workspace, userId);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] int? userId, [FromQuery] string? name)
    {
        var filter = userId is null && name is null
            ? null 
            : new WorkspaceFilter { UserId = userId, Name = name };
        
        var workspaces = await workspaceService.GetAllWorkspacesAsync(filter);
        return Ok(workspaces);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var workspace = await workspaceService.GetWorkspace(id);
        return workspace is null ? NotFound() : Ok(workspace);
    }
}