using Glassdoor.DataAccess.Configurations;
using Glassdoor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.DataAccess.Contexts;

public class GlassdoorDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"Host={Constants.HOST};Port={Constants.PORT};Database={Constants.DATABASE};User Id={Constants.USER};Password={Constants.PASSWORD};";
        optionsBuilder.UseNpgsql(connectionString);
    }
}
