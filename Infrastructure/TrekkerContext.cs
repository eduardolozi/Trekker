using Domain.Models;
using Domain.Utils;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class TrekkerContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Workspace> Workspace { get; set; }
    public DbSet<Chat> Chat { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<MessageMedia> MessageMedia { get; set; }
    public DbSet<Team> Team { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!IsRunningInContainer()) Env.Load("../.env");
        optionsBuilder.UseNpgsql(TrekkerEnvironment.TrekkerPostgresConnectionString);
    }
    
    private static bool IsRunningInContainer()
    {
        return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    }
}