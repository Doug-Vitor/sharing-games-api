using Core.Entities;
using Core.Response;

namespace Core.Interfaces;

public interface IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  where T : BaseEntity where TViewModel : class where TInputModel : class where TUpdateModel : class
{
  IWritableRepository<T> WritableRepository { get; init; }
  Task<ActionResponse> InsertAsync(TInputModel inputModel);
  Task<ActionResponse> InsertAsync(IEnumerable<TInputModel> inputModels);
  Task<ActionResponse> UpdateAsync(int? id, TUpdateModel inputModel);
  Task<ActionResponse> UpdateAsync(IEnumerable<TUpdateModel> inputModels);
  Task<ActionResponse> RemoveAsync(int? id);
  Task<ActionResponse> RemoveAsync(IEnumerable<int>? ids);
}