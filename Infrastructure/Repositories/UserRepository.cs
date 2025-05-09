﻿using Domain.Filters;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(TrekkerContext db) : IUserRepository
{
    public async Task InsertUser(User user)
    {
        await db.User.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public Task<User?> FindUser(int id, UserFilter filter)
    {
        var query = db.User.AsNoTracking().AsQueryable();
        if (filter.IncludeWorkspaces) query = query.Include(x => x.Workspaces);
        return query
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task PutPhoto(int id, string fileKey)
    {
        var user = await db.User.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("The user could not be found.");
        
        user.PhotoPath = fileKey;
        await db.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => db.SaveChangesAsync();
}