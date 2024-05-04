using Core.Entities;
using Core.Response;
using Core.V1;

namespace Core.Interfaces;

public interface ISanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  where T : BaseEntity where TViewModel : class where TInputModel : class where TUpdateModel : class
{
  IReadonlySanitizerService<T, TViewModel> ReadonlySanitizerService { get; init; }
  IWritableSanitizerService<T, TViewModel, TInputModel, TUpdateModel> WritableSanitizerService { get; init; }
  IValidatorService<TInputModel, TUpdateModel> Validator { get; init; }

  Task<ActionResponse> InsertAsync(TInputModel inputModel);
  Task<ActionResponse> InsertAsync(IEnumerable<TInputModel> inputModels);
  Task<ActionResponse> GetByIdAsync(int? id, IEnumerable<string>? propertyNamesToBeIncluded);
  Task<ActionResponse> GetAllAsync(SearchParams<T>? searchParams, IEnumerable<string>? propertyNamesToBeIncluded);
  Task<ActionResponse> UpdateAsync(int? id, TUpdateModel inputModel);
  Task<ActionResponse> UpdateAsync(IEnumerable<TUpdateModel> inputModels);
  Task<ActionResponse> RemoveAsync(int? id);
  Task<ActionResponse> RemoveAsync(IEnumerable<int>? ids);
}