using Microsoft.AspNetCore.Mvc;
using Trekker.Application.Interfaces;
using Trekker.Domain.DTOs;

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
}