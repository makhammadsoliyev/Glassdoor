using Glassdoor.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Glassdoor.DataAccess.Contexts;

public class GlassdoorDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"Host={Constants.HOST};Port={Constants.PORT};Database={Constants.DATABASE};User Id={Constants.USER};Password={Constants.PASSWORD};";
        optionsBuilder.UseNpgsql(connectionString);
    }
    public DbSet<User> Users { get; set; }
}
