using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService teamService) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult> Create([FromBody] Team team, [FromQuery] int creatorUserId)
    {
        await teamService.CreateTeam(team, creatorUserId);
        return Ok();
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult> Create([FromRoute] int id)
    {
        var team = await teamService.GetTeam(id);
        return team is null ? NotFound() : Ok(team);
    }
    
    [HttpPost("{teamId}/add-user")]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult> AddUserToTeam([FromRoute] int teamId, [FromBody] AddUserRequest addUserRequest)
    {
        await teamService.AddUserToTeam(teamId, addUserRequest);
        return Ok();
    }
}