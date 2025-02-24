using Trekker.Domain.Models;

namespace Trekker.Infrastructure.Repositories;

public class UserRepository(TrekkerDbContext context)
{
    public Task Add(User user)
    {
        return context.AddAsync(user).AsTask();
    }
}