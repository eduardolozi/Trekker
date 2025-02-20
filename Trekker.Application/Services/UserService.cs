using Trekker.Application.Interfaces;
using Trekker.Domain.DTOs;
using Trekker.Domain.Interfaces;
using Trekker.Domain.Models;

namespace Trekker.Application.Services;

public class UserService(IKeycloakService keycloakService) : IUserService
{
    public async Task Register(KeycloakRegisterDTO registerDto)
    {
        await keycloakService.CreateUser(registerDto);
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