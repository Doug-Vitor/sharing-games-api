using Core.Entities;
using Core.V1.DTOs;

namespace Core.Interfaces;

public interface IAuthenticatedSanitizerService<T, TViewModel, TInputModel, TUpdateModel> : ISanitizerService<T, TViewModel, TInputModel, TUpdateModel>
  where T : BaseEntity, IOwnedByUser where TViewModel : ViewModel, IOwnedByUser where TInputModel : class, IOwnedByUser where TUpdateModel : class, IKeyed
{ }