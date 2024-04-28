using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class WritableRepository<T> : IWritableRepository<T> where T : BaseEntity
{
  public readonly DbSet<T> Collection;
  protected readonly AppDbContext Context;

  public WritableRepository(AppDbContext dbContext)
    => (Context, Collection) = (dbContext, dbContext.Set<T>());

  public async Task<T> InsertAsync(T entity)
  {
    await Collection.AddAsync(entity);
    await Context.SaveChangesAsync();
    return entity;
  }

  public async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
  {
    await Collection.AddRangeAsync(entities);
    await Context.SaveChangesAsync();
    return entities;
  }

  public async Task<T> UpdateAsync(int id, T entity)
  {
    entity.Id = id;
    Context.Entry(entity).CurrentValues.SetValues(entity);

    Context.Update(entity);
    await Context.SaveChangesAsync();
    return entity;
  }

  public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
  {
    Context.UpdateRange(entities);
    await Context.SaveChangesAsync();
    return entities;
  }

  public async Task RemoveAsync(int id)
  {
    await Context.Set<T>()
      .Where(entity => entity.Id == id)
      .ExecuteDeleteAsync();
  }

  public async Task RemoveAsync(IEnumerable<int> ids)
    => await Context.Set<T>()
                    .Where(entity => ids.Contains(entity.Id))
                    .ExecuteDeleteAsync();
}