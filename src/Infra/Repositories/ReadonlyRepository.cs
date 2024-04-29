using System.Linq.Dynamic.Core;
using Core.Entities;
using Core.Interfaces;
using Core.V1;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ReadonlyRepository<T> : IReadonlyRepository<T> where T : BaseEntity
{
  public readonly DbSet<T> Collection;

  public ReadonlyRepository(AppDbContext dbContext) => Collection = dbContext.Set<T>();

  public async Task<T?> GetByIdAsync(int? id, IEnumerable<string> propertyNamesToBeIncluded)
    => await IncludeProperties(Collection.Where(x => x.Id == id), propertyNamesToBeIncluded).FirstOrDefaultAsync();

  public async Task<ICollection<T>> GetAllAsync(SearchParams<T>? searchParams, IEnumerable<string> propertyNamesToBeIncluded)
  {
    int? cursor = searchParams?.Cursor;
    searchParams?.ApplyCustomPredicates();

    IQueryable<T> query = Collection.OrderBy($"{searchParams?.SortBy} {searchParams?.SortDirection}")
                                    .Where(x => x.Id > cursor!)
                                    .Where(searchParams?.CustomPredicates)
                                    .Take(searchParams?.PerPage ?? 25);

    return await IncludeProperties(query, propertyNamesToBeIncluded).ToListAsync();

  }

  static IQueryable<T> IncludeProperties(IQueryable<T> query, IEnumerable<string> propertyNamesToBeIncluded)
  {
    foreach (string propertyName in propertyNamesToBeIncluded)
      query = query.Include(propertyName);

    return query.AsNoTracking().AsSplitQuery();
  }
}