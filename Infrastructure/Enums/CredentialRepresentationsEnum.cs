using System.Text.Json.Serialization;

namespace Infrastructure.Enums;

public enum CredentialRepresentationsEnum
{
    password,
    secret,
    totp
}