using Trekker.Domain.Models;

namespace Trekker.Domain.Interfaces;

public interface IUserRepository
{
    Task Add(User user);
    Task AddPhoto(string keycloakId, string photoUrl);
}