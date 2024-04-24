using Core.Entities;
using Core.Response;
using Core.V1;

namespace Core.Interfaces;

public interface ISanitizerService<T> where T : BaseEntity
{
  Task<SuccessResponse<T>> GetByIdAsync(int? id);
  Task<SuccessResponse<ICollection<T>>> GetAllAsync(SearchParams<T>? searchParams);
}