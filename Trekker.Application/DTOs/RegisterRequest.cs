using Microsoft.AspNetCore.Http;

namespace Trekker.Application.DTOs;

public class RegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IFormFile? Photo { get; set; } 
}