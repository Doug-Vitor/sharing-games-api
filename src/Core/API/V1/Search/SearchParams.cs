using System.Linq.Expressions;
using Core.Entities;

namespace Core.V1;

public class SearchParams<T>() where T : BaseEntity
{
  public string? SortBy { get; set; } = "Id";
  public string? SortDirection { get; set; } = "ASC";
  public int? Cursor { get; set; } = 0;
  public int? PerPage { get; set; } = 25;
  public Expression<Func<T, bool>>? CustomPredicates { get; set; }

  public virtual void ApplyCustomPredicates() => CustomPredicates = _ => true;
}