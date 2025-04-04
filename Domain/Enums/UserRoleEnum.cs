﻿using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoleEnum
{
    Admin,
    Manager,
    Member
}