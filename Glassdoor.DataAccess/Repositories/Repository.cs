using Glassdoor.DataAccess.Contexts;
using Glassdoor.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace Glassdoor.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private GlassdoorDbContext context;
    private readonly DbSet<TEntity> entities;

    public Repository(GlassdoorDbContext context)
    {
        this.context = context;
        this.entities = context.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        return (await entities.AddAsync(entity)).Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entities.Entry(entity).State = EntityState.Modified;
        return await Task.FromResult(entity);
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        return await Task.FromResult(entities.Remove(entity).Entity);
    }

    public async Task<TEntity> SelectByIdAsync(long id)
    {
#pragma warning disable
        return await entities.FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted);
#pragma warning enable
    }

    public IEnumerable<TEntity> SelectAllAsEnumerable()
    {
        return entities.Where(entity => !entity.IsDeleted).AsEnumerable();
    }

    public IQueryable<TEntity> SelectAllAsQueryable()
    {
        return entities.Where(entity => !entity.IsDeleted).AsQueryable();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
