namespace TechxManagementApi.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using TechxManagementApi.Entities;

public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Task> Tasks { get; set; }
    
    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgresql database
        #if (DEBUG)

            options.UseNpgsql(Configuration["AppSettings:TECHX_DB_CONNECTION_STRING"]);

        #else

            options.UseNpgsql(Environment.GetEnvironmentVariable("TECHX_DB_CONNECTION_STRING"));

        #endif
    }
}