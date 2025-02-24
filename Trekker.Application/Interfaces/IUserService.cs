using Microsoft.AspNetCore.Http;
using Trekker.Domain.DTOs;
using Trekker.Domain.Models;

namespace Trekker.Application.Interfaces;

public interface IUserService
{
    Task Register(KeycloakRegisterDTO registerDto);
    Task AddPhoto(string id, IFormFile file);
    Task<KeycloakTokenResponse> Login(LoginDTO login);
    Task<KeycloakTokenResponse> RefreshLogin(string refreshToken);
}