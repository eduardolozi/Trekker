using Microsoft.AspNetCore.Mvc;
using Trekker.Application.Interfaces;
using Trekker.Domain.DTOs;
using Trekker.Domain.Models;

namespace Trekker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] KeycloakRegisterDTO registerDto)
    {
        await userService.Register(registerDto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<KeycloakTokenResponse>> Login([FromBody] LoginDTO login)
    {
        var loginResponse = await userService.Login(login);
        return Ok(loginResponse);
    }

    [HttpPost("refresh-login")]
    public async Task<ActionResult<KeycloakTokenResponse>> RefreshLogin([FromBody] string refreshToken)
    {
        var loginResponse = await userService.RefreshLogin(refreshToken);
        return Ok(loginResponse);
    }
}