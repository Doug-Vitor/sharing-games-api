using System.Linq.Expressions;
using Core.Entities;

namespace Core.V1;

public class SearchParams<T>() where T : BaseEntity
{
  public readonly string OrderBy = "Id";
  public readonly int Cursor;
  public readonly Expression<Func<T, bool>>? CustomPredicates;

  public SearchParams(int cursor, Expression<Func<T, bool>>? customPredicates) : this()
    => (Cursor, CustomPredicates) = (cursor, customPredicates);
}