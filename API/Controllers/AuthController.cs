using Application.DTOs;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginDTO login)
    {
        var tokens = await authService.Login(login);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax
        };
        Response.Cookies.Append("AccessToken", tokens.access_token, cookieOptions);
        Response.Cookies.Append("RefreshToken", tokens.refresh_token!, cookieOptions);
        return Ok();
    } 
    
    [HttpPost("refresh")]
    public async Task<ActionResult<TokenDTO>> RefreshLogin()
    {
        Request.Cookies.TryGetValue("RefreshToken", out var refreshToken);
        if(refreshToken == null) return Unauthorized();
        
        var tokens = await authService.RefreshAccessToken(refreshToken);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax
        };
        Response.Cookies.Append("AccessToken", tokens.access_token, cookieOptions);
        Response.Cookies.Append("RefreshToken", tokens.refresh_token!, cookieOptions);
        return Ok(tokens);
    } 
}