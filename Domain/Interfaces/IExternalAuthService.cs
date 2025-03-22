using Domain.DTOs;
using Domain.Models;

namespace Domain.Interfaces;

public interface IExternalAuthService
{
    public Task<string> RegisterUser(User user);
    public Task<TokenDTO> Login(LoginDTO login);
    public Task<TokenDTO> RefreshAccessToken(string refreshToken);
}