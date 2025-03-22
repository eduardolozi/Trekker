using Application.DTOs;
using Domain.DTOs;
using Domain.Interfaces;

namespace Application.Services;

public class AuthService(IExternalAuthService externalAuthService)
{
    public Task<TokenDTO> Login(LoginDTO login)
    {
        return externalAuthService.Login(login);
    }
    
    public Task<TokenDTO> RefreshAccessToken(string refreshToken)
    {
        return externalAuthService.RefreshAccessToken(refreshToken);
    }
}