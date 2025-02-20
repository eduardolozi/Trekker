using Trekker.Domain.DTOs;
using Trekker.Domain.Models;

namespace Trekker.Domain.Interfaces;

public interface IKeycloakService
{ 
    Task CreateUser(KeycloakRegisterDTO registerDto);
    Task<KeycloakRegisterDTO> GetUserByUsername(string username);
    Task<List<KeycloakRoleDTO>> GetRoles();
    Task RegisterRole(string keycloakUserId, string roleName);
    Task<KeycloakTokenResponse> GetUserTokens(LoginDTO login);
    Task<KeycloakTokenResponse> RefreshUserToken(string refreshToken);
}