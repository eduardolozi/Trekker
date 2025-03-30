using Application.Interfaces;
using Application.Services;
using Domain.DTOs;
using Domain.Filters;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            await userService.CreateUser(user);
            return Ok();
        }

        [HttpPut("{id}/photo")]
        public async Task<ActionResult> PutPhoto([FromRoute] int id, [FromForm] IFormFile photo)
        {
            var file = new FileDTO
            {
                ContentType = photo.ContentType,
                FileName = photo.FileName,
                Stream = photo.OpenReadStream()
            };
            
            await userService.PutPhoto(id, file);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<User>> Get([FromRoute] int id, [FromBody] UserFilter filter)
        {
            var user = await userService.GetUser(id, filter);
            return user is null ? NotFound() : Ok(user);
        }
    }
}
