using Trekker.Domain.DTOs;
using Trekker.Domain.Models;

namespace Trekker.Domain.Mappers;

public static class UserMapper
{
    public static User KeycloakRegisterToUser(KeycloakRegisterDTO registerDto)
    {
        return new User
        {
            FullName = $"{registerDto.FirstName} {registerDto.LastName}",
            Username = registerDto.Username,
            Email = registerDto.Email,
            KeycloakId = registerDto.Id!
        };
    }
}