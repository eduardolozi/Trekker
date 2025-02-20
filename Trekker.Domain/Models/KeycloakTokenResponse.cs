using System.Text.Json.Serialization;

namespace Trekker.Domain.Models;

public class KeycloakTokenResponse
{
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public string token_type { get; set; }
    public string? refresh_token { get; set; }
    public int? refresh_expires_in { get; set; }
    [JsonPropertyName("not-before-police")] public int? not_before_policy { get; set; }
    public string? session_sate { get; set; }
    public string? scope { get; set; }
}