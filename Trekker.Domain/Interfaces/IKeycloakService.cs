using Trekker.Domain.DTOs;

namespace Trekker.Domain.Interfaces;

public interface IKeycloakService
{ 
    Task CreateUser(KeycloakRegisterDTO registerDto);
    Task<KeycloakRegisterDTO> GetUserByUsername(string username);
    Task<List<KeycloakRoleDTO>> GetRoles();
    Task RegisterRole(string keycloakUserId, string roleName);
}