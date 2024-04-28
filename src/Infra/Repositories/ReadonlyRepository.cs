using Core.Entities;
using Core.Interfaces;
using Core.V1;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ReadonlyRepository<T> : IReadonlyRepository<T> where T : BaseEntity
{
  public readonly DbSet<T> Collection;

  public ReadonlyRepository(AppDbContext dbContext) => Collection = dbContext.Set<T>();

  public async Task<T?> GetByIdAsync(int? id) => await Collection.FirstOrDefaultAsync(t => t.Id == id);

  public async Task<ICollection<T>> GetAllAsync(SearchParams<T>? searchParams)
  {
    IQueryable<T> query = Collection.Where(x => x.Id > searchParams.Cursor);

    if (searchParams.CustomPredicates is not null) query = query.Where(searchParams.CustomPredicates);
    return await query.ToListAsync();
  }
}