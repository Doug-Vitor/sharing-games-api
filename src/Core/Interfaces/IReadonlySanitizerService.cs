using Core.Entities;
using Core.Response;
using Core.V1;

namespace Core.Interfaces;

public interface IReadonlySanitizerService<T, TViewModel> where T : BaseEntity where TViewModel : class
{
  IReadonlyRepository<T> ReadonlyRepository { get; init; }
  Task<ActionResponse> GetByIdAsync(int? id);
  Task<ActionResponse> GetAllAsync(SearchParams<T>? searchParams);
}