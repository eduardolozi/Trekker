using System.Text.Json.Serialization;

namespace Trekker.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoleEnum
{
    manager,
    leader,
    common
}