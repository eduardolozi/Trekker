using Trekker.Domain.Enums;

namespace Trekker.Domain.DTOs;

public class KeycloakRegisterDTO
{
    public string? Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Dictionary<string, List<object>> Attributes { get; set; }
    public List<KeycloakUserCredentialsDTO> Credentials { get; set; }
    public bool Enabled { get; init; } = true;
    public List<UserRoleEnum> RealmRoles { get; set; }
}