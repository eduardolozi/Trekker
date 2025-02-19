using Trekker.Application.DTOs;

namespace Trekker.Application.Interfaces;

public interface IUserService
{
    Task Register(RegisterRequest registerRequest);
}