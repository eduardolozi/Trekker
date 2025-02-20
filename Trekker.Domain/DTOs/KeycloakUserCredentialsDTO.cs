namespace Trekker.Domain.DTOs;

public class KeycloakUserCredentialsDTO
{
    public string Type { get; init; } = "Password";
    public string Value { get; set; }
    public bool Temporary { get; init; } = false;
}