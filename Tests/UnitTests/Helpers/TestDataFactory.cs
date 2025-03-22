using Domain.Enums;
using Domain.Models;

namespace Tests.UnitTests.Helpers;

public static class TestDataFactory
{
    public static User GetValidAdminUser()
    {
        return new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            Username = "johnsmith",
            Email = "johnsmith@gmail.com",
            Role = UserRoleEnum.Admin
        };
    }
    
    public static User GetValidManagerUser()
    {
        return new User
        {
            Id = 2,
            FirstName = "Richard",
            LastName = "Doe",
            Username = "richarddoe",
            Email = "richarddoe@gmail.com",
            Role = UserRoleEnum.Manager
        };
    }
    
    public static User GetValidMemberUser()
    {
        return new User
        {
            Id = 3,
            FirstName = "Steve",
            LastName = "Cooper",
            Username = "stevecooper",
            Email = "stevecooper@gmail.com",
            Role = UserRoleEnum.Member
        };
    }
    
    public static Team GetValidEmptyTeam()
    {
        return new Team
        {
            Id = 1,
            Name = "Test Team",
            WorkspaceId = 1
        };
    }

    public static Team GetValidTeamWithUser()
    {
        return new Team
        {
            Id = 1,
            Name = "Test Team",
            WorkspaceId = 1,
            Users = [GetValidManagerUser()]
        };
    }

    public static Workspace GetValidWorkspace()
    {
        return new Workspace
        {
            Id = 1,
            Name = "Test Workspace"
        };
    }
}