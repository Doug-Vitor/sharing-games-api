using Core.Entities;

namespace Core.Interfaces;

public interface IWritableRepository<T> where T : BaseEntity
{
  Task<T> InsertAsync(T entity);
  Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities);
  Task<T> UpdateAsync(int id, T entity);
  Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);
  Task RemoveAsync(int id);
  Task RemoveAsync(IEnumerable<int> ids);
}