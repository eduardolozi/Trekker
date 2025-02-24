using Microsoft.AspNetCore.Http;
using Trekker.Application.Interfaces;
using Trekker.Domain.DTOs;
using Trekker.Domain.Interfaces;
using Trekker.Domain.Mappers;
using Trekker.Domain.Models;

namespace Trekker.Application.Services;

public class UserService(IKeycloakService keycloakService, IUserRepository userRepository) : IUserService
{
    public async Task Register(KeycloakRegisterDTO registerDto)
    {
        await keycloakService.CreateUser(registerDto);
        var user = UserMapper.KeycloakRegisterToUser(registerDto);
        await userRepository.Add(user);
    }

    public async Task AddPhoto(string id, IFormFile file)
    {
        
        // await userRepository.Add(id, photoUrl);
    }

    public Task<KeycloakTokenResponse> Login(LoginDTO login)
    {
        return keycloakService.GetUserTokens(login);
    }

    public Task<KeycloakTokenResponse> RefreshLogin(string refreshToken)
    {
        return keycloakService.RefreshUserToken(refreshToken);
    }
}