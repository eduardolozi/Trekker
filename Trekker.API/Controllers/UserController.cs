using Microsoft.AspNetCore.Mvc;
using Trekker.Application.Interfaces;
using Trekker.Domain.Utils;
using RegisterRequest = Trekker.Application.DTOs.RegisterRequest;

namespace Trekker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAdminToken()
    {
        var client = new HttpClient();
        var tokenRequest = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", TrekkerEnvironment.KcClientId },
            { "client_secret", TrekkerEnvironment.KcClientSecret },
        };
        var form = new FormUrlEncodedContent(tokenRequest);
        var response = await client.PostAsync("http://localhost:8080/realms/trekker/protocol/openid-connect/token", form);
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadFromJsonAsync<dynamic>());
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegisterRequest registerRequest)
    {
        await userService.Register(registerRequest);
        return Ok();
    }
}