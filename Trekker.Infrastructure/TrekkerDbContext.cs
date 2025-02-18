using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Trekker.Domain.Models;
using Trekker.Domain.Utils;

namespace Trekker.Infrastructure;

public class TrekkerDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Env.Load("../.env");
        optionsBuilder.UseNpgsql(TrekkerEnvironment.TrekkerPostgresConnectionString);
    }
    
    public DbSet<Workspace> Worksapce { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Permission> Permission { get; set; }
    public DbSet<Chat> Chat { get; set; }
    public DbSet<ChatMessage> ChatMessage { get; set; }
    public DbSet<ChatActivity> ChatActivitie { get; set; }
    public DbSet<Team> Team { get; set; }
}