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
    public DbSet<AccountTask> AccountTasks { get; set; }
    public DbSet<TeamTask> TeamTasks { get; set; }
    
    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Companies)
                .WithMany(c => c.Employees);
            modelBuilder.Entity<Company>()
                .HasOne(o => o.Owner)
                .WithMany(c => c.OwnsCompanies);
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Projects)
                .WithOne(p => p.Team);
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Tasks)
                .WithOne(tt => tt.AssignedTo);
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Tasks)
                .WithOne(t => t.AssignedTo);
            modelBuilder.Entity<Project>()
                .HasMany(p => p.TeamTasks)
                .WithOne(t => t.Project);
            modelBuilder.Entity<Project>()
                .HasMany(p => p.AccountTasks)
                .WithOne(t => t.Project);
        }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseLazyLoadingProxies();
        // connect to postgresql database
        #if (DEBUG)

            options.UseNpgsql(Configuration["AppSettings:TECHX_DB_CONNECTION_STRING"]);

        #else

            options.UseNpgsql(Environment.GetEnvironmentVariable("TECHX_DB_CONNECTION_STRING"));

        #endif
    }
}