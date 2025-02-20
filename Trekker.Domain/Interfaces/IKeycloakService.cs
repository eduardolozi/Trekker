using Trekker.Domain.DTOs;

namespace Trekker.Domain.Interfaces;

public interface IKeycloakService
{ 
    Task CreateUser(KeycloakRegisterDTO registerDto);
}