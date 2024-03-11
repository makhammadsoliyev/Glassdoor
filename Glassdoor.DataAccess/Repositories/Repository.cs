using Glassdoor.DataAccess.Contexts;
using Glassdoor.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return await Task.FromResult(entities.Remove(entity).Entity);
    }
    public async Task<TEntity> SelectByIdAsync(long id)
    {
#pragma warning disable
        return await entities.FirstOrDefaultAsync(entity => entity.Id == id);
#pragma warning enable
    }
    public IEnumerable<TEntity> SelectAllAsEnumerable()
    {
        return entities.AsEnumerable();
    }
    public IQueryable<TEntity> SelectAllAsQueryable()
    {
        return entities.AsQueryable();
    }
    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}
