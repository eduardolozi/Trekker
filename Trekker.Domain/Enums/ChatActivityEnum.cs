using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Trekker.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatActivityEnum
{
    Join,
    Left,
    ChangedDescription
}