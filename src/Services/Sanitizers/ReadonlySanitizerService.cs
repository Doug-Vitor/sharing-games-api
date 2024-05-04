using System.Net;
using Core.Entities;
using Core.Interfaces;
using Core.Response;
using Core.V1;

namespace Services;

public class ReadonlySanitizerService<T, TViewModel>
  : IReadonlySanitizerService<T, TViewModel> where T : BaseEntity where TViewModel : class
{
  public IReadonlyRepository<T> ReadonlyRepository { get; init; }

  public ReadonlySanitizerService(IReadonlyRepository<T> readonlyRepository)
    => ReadonlyRepository = readonlyRepository;

  public async Task<ActionResponse> GetByIdAsync(int? id, IEnumerable<string>? propertyNamesToBeIncluded)
  {
    ArgumentNullException.ThrowIfNull(id);
    T? foundEntity = await ReadonlyRepository.GetByIdAsync(id, propertyNamesToBeIncluded ?? Enumerable.Empty<string>());
    if (foundEntity is null) return new ActionResponse((int)HttpStatusCode.NotFound);
    return new SuccessResponse<TViewModel>((int)HttpStatusCode.OK, (TViewModel)(foundEntity as dynamic));
  }

  public async Task<ActionResponse> GetAllAsync(SearchParams<T>? searchParams, IEnumerable<string>? propertyNamesToBeIncluded)
  {
    IEnumerable<T> entities = await ReadonlyRepository.GetAllAsync(searchParams, propertyNamesToBeIncluded ?? Enumerable.Empty<string>());
    return new SuccessResponse<IEnumerable<TViewModel>>(
      (int)HttpStatusCode.OK,
      entities.Select(entity => (TViewModel)(entity as dynamic))
    );
  }
}