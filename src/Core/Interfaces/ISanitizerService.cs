using Core.Entities;
using Core.Response;
using Core.V1;

namespace Core.Interfaces;

public interface ISanitizerService<T> where T : BaseEntity
{
  Task<ActionResponse> GetByIdAsync(int? id);
  Task<ActionResponse> GetAllAsync(SearchParams<T>? searchParams);
}