using Domain.DTOs;
using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces;

public interface IUserService
{
    Task CreateUser(User user);
    Task PutPhoto(int id, FileDTO file);
    Task<User?> GetUser(int id, UserFilter filter);
}