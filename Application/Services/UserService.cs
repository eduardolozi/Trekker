using Application.Interfaces;
using Domain.DTOs;
using Domain.Filters;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class UserService(IExternalAuthService externalAuthService, IUserRepository userRepository) : IUserService
{
    public async Task CreateUser(User user)
    {
        try
        {
            user.KeycloakId = await externalAuthService.RegisterUser(user);
            await userRepository.InsertUser(user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    
    public Task<User?> GetUser(int id, UserFilter filter)
    {
        return userRepository.FindUser(id, filter);
    }
}