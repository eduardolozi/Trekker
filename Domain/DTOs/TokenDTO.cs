﻿namespace Domain.DTOs;

public class TokenDTO
{
    public string access_token { get; set; }
    public string? refresh_token { get; set; }
}