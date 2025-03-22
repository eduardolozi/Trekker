using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface ITeamService
{
    Task CreateTeam(Team team, int creatorUserId);
    Task<Team?> GetTeam(int id);
    Task AddUserToTeam(int teamId, AddUserRequest addUserRequest);
}