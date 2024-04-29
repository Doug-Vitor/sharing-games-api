using Core.Entities;
using Core.V1;

namespace Core.Interfaces;

public interface IReadonlyRepository<T> where T : BaseEntity
{
  Task<T?> GetByIdAsync(int? id, IEnumerable<string> propertyNamesToBeIncluded);
  Task<ICollection<T>> GetAllAsync(SearchParams<T>? searchParams, IEnumerable<string> propertyNamesToBeIncluded);
}