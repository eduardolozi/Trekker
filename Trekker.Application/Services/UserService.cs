using Trekker.Application.Interfaces;
using Trekker.Domain.DTOs;
using Trekker.Domain.Interfaces;

namespace Trekker.Application.Services;

public class UserService(IKeycloakService keycloakService) : IUserService
{
    public async Task Register(KeycloakRegisterDTO registerDto)
    {
        await keycloakService.CreateUser(registerDto);
    }
}