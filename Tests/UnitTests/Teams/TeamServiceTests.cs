using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Filters;
using Domain.Interfaces;
using Moq;
using Tests.UnitTests.Helpers;

namespace Tests.UnitTests.Teams;

public class TeamServiceTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly TeamService _teamService;
    public TeamServiceTests()
    {
        _teamRepositoryMock = new Mock<ITeamRepository>();
        _userServiceMock = new Mock<IUserService>();
        _teamService = new TeamService(_teamRepositoryMock.Object, _userServiceMock.Object);
    }
    
    [Fact]
    public async Task CreateTeam_AddsCreatorToTeamSuccessfully()
    {
        var user = TestDataFactory.GetValidAdminUser();
        var team = TestDataFactory.GetValidEmptyTeam();

        _userServiceMock
            .Setup(x => x.GetUser(user.Id, It.IsAny<UserFilter>()))
            .ReturnsAsync(user);
        
        await _teamService.CreateTeam(team, user.Id);
        
        _teamRepositoryMock.Verify(x => x.InsertTeam(team), Times.Once);
        Assert.Single(team.Users);
        Assert.Contains(user, team.Users);
    }

    [Fact]
    public async Task AddUserToTeam_WhenUserIsNotInTeam_AddsUserSuccessfully()
    {
        var user = TestDataFactory.GetValidMemberUser();
        var team = TestDataFactory.GetValidTeamWithUser();
        var addUserRequest = new AddUserRequest { UserId = user.Id };
        
        _teamRepositoryMock
            .Setup(x => x.FindTeam(team.Id, It.IsAny<bool>()))
            .ReturnsAsync(team);
        
        _userServiceMock
            .Setup(x => x.GetUser(user.Id, It.IsAny<UserFilter>()))
            .ReturnsAsync(user);
        
        await _teamService.AddUserToTeam(team.Id, addUserRequest);
        
        Assert.Contains(user, team.Users);
        Assert.Equal(2, team.Users.Count);
    }
    
    [Fact]
    public async Task AddUserToTeam_WhenUserIsInTeam_DontAddsUser()
    {
        var user = TestDataFactory.GetValidManagerUser();
        var team = TestDataFactory.GetValidTeamWithUser();
        var addUserRequest = new AddUserRequest { UserId = user.Id };
        
        _teamRepositoryMock
            .Setup(x => x.FindTeam(team.Id, It.IsAny<bool>()))
            .ReturnsAsync(team);
        
        _userServiceMock
            .Setup(x => x.GetUser(user.Id, It.IsAny<UserFilter>()))
            .ReturnsAsync(user);
        
        var ex = await Assert.ThrowsAsync<Exception>(() => _teamService.AddUserToTeam(team.Id, addUserRequest));
        
        Assert.Equal("User is already in team", ex.Message);
        Assert.Single(team.Users);
    }
}