using Application.DTOs;
using Application.Interfaces;
using Domain.Filters;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class TeamService(ITeamRepository teamRepository, IUserService userService) : ITeamService
{
    public async Task CreateTeam(Team team, int creatorUserId)
    {
        var user = await userService.GetUser(creatorUserId, new UserFilter {IncludeWorkspaces = false})
                   ?? throw new Exception("The user could not be found");

        team.Users = [user];
        await teamRepository.InsertTeam(team);
    }

    public Task<Team?> GetTeam(int id)
    {
        return teamRepository.FindTeam(id, trackChanges: false);
    }

    public async Task AddUserToTeam(int teamId, AddUserRequest addUserRequest)
    {
        var team = await teamRepository.FindTeam(teamId, trackChanges: true) 
                   ?? throw new Exception("The team could not be found");

        if (IsUserInTeam(team, addUserRequest.UserId)) throw new Exception("User is already in team");
        
        var user = await userService.GetUser(addUserRequest.UserId, new UserFilter { IncludeWorkspaces = false }) 
                   ?? throw new Exception("The user could not be found.");
        
        team.Users.Add(user);
        await teamRepository.SaveChangesAsync();
    }

    private static bool IsUserInTeam(Team team, int userId)
    {
        return team.Users.Any(x => x.Id == userId);
    }
}