using Domain.Filters;
using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task InsertUser(User user);
    Task<User?> FindUser(int id, UserFilter filter);
    Task PutPhoto(int id, string fileKey); 
    Task SaveChangesAsync();
}