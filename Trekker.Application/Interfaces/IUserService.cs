using Trekker.Domain.DTOs;

namespace Trekker.Application.Interfaces;

public interface IUserService
{
    Task Register(KeycloakRegisterDTO registerDto);
}