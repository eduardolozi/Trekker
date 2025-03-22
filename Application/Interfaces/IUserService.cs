using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces;

public interface IUserService
{
    Task CreateUser(User user);
    Task<User?> GetUser(int id, UserFilter filter);
}