using Glassdoor.DataAccess.Configurations;
using Glassdoor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Glassdoor.DataAccess.Contexts;

public class GlassdoorDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Application> Applications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"Host={Constants.HOST};Port={Constants.PORT};Database={Constants.DATABASE};User Id={Constants.USER};Password={Constants.PASSWORD};";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>()
            .HasOne(application => application.User)
            .WithMany(user => user.Applications)
            .HasForeignKey(application => application.UserId);

        modelBuilder.Entity<Application>()
            .HasOne(application => application.Job)
            .WithMany(job => job.Applications)
            .HasForeignKey(application => application.JobId);

        modelBuilder.Entity<Job>()
            .HasOne(job => job.Company)
            .WithMany(company => company.Jobs)
            .HasForeignKey(company => company.CompanyId);
    }
}
