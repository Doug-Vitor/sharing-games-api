using System.Net;
using Core.Entities;
using Core.Interfaces;
using Core.Response;

namespace Services;

public class WritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  : IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  where T : BaseEntity where TViewModel : class where TInputModel : class where TUpdateModel : class
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
    return new SuccessResponse<TViewModel>((int)HttpStatusCode.Created, (TViewModel)(updatedEntity as dynamic));
  }

  public async Task<ActionResponse> UpdateAsync(IEnumerable<TUpdateModel> inputModels)
  {
    var entities = await WritableRepository.UpdateAsync(
      inputModels.Select(inputModel => (T)(inputModel as dynamic))
    );

    return new SuccessResponse<IEnumerable<TViewModel>>(
      (int)HttpStatusCode.Created,
      entities.Select(entity => (TViewModel)(entity as dynamic))
    );
  }

  public Task<ActionResponse> RemoveAsync(int? id)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResponse> RemoveAsync(IEnumerable<int>? ids)
  {
    throw new NotImplementedException();
  }
}