using Core.V1;
using Core.Entities;

namespace Core.Interfaces;

public interface IReadonlyRepository<T> where T : BaseEntity
{
  Task<T?> GetByIdAsync(int? id);
  Task<ICollection<T>> GetAllAsync(SearchParams<T>? searchParams);
}