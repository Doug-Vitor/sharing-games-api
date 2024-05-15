using System.Net;
using Core.Entities;
using Core.Interfaces;
using Core.Response;
using Microsoft.IdentityModel.Tokens;

namespace Services;

public class WritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  : IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  where T : BaseEntity where TViewModel : class where TInputModel : class where TUpdateModel : class, IKeyed
{
  public IWritableRepository<T> WritableRepository { get; init; }

  public WritableSanitizerService(IWritableRepository<T> writableRepository)
    => WritableRepository = writableRepository;

  public async Task<ActionResponse> InsertAsync(TInputModel inputModel)
  {
    var createdEntity = await WritableRepository.InsertAsync((T)(inputModel as dynamic));
    return new SuccessResponse<TViewModel>((int)HttpStatusCode.Created, (TViewModel)(createdEntity as dynamic));
  }

  public async Task<ActionResponse> InsertAsync(IEnumerable<TInputModel> inputModels)
  {
    var entities = await WritableRepository.InsertAsync(
      inputModels.Select(inputModel => (T)(inputModel as dynamic))
    );

    return new SuccessResponse<IEnumerable<TViewModel>>(
      (int)HttpStatusCode.Created,
      entities.Select(entity => (TViewModel)(entity as dynamic))
    );
  }

  public async Task<ActionResponse> UpdateAsync(int? id, TUpdateModel inputModel)
  {
    ArgumentNullException.ThrowIfNull(id);
    var updatedEntity = await WritableRepository.UpdateAsync(id.Value, (T)(inputModel as dynamic));
    return new SuccessResponse<TViewModel>((int)HttpStatusCode.OK, (TViewModel)(updatedEntity as dynamic));
  }

  public async Task<ActionResponse> UpdateAsync(IEnumerable<TUpdateModel> inputModels)
  {
    var entities = await WritableRepository.UpdateAsync(
      inputModels.Select(inputModel => (T)(inputModel as dynamic))
    );

    return new SuccessResponse<IEnumerable<TViewModel>>(
      (int)HttpStatusCode.OK,
      entities.Select(entity => (TViewModel)(entity as dynamic))
    );
  }

  public async Task<ActionResponse> RemoveAsync(int? id)
  {
    if (id.HasValue)
    {
      await WritableRepository.RemoveAsync(id.Value);
      return new((int)HttpStatusCode.NoContent);
    }

    return GetErrorResponse(nameof(id));
  }

  public async Task<ActionResponse> RemoveAsync(IEnumerable<int>? ids)
  {
    if (ids.IsNullOrEmpty()) return GetErrorResponse(nameof(ids));
    await WritableRepository.RemoveAsync(ids!);
    return new((int)HttpStatusCode.NoContent);
  }

  static ErrorResponse GetErrorResponse(string key) => new(
    (int)HttpStatusCode.BadRequest,
    new Dictionary<string, string[]>()
    {
      { key, [$"Invalid {key} provided"] }
    }
  );
}