namespace Tests.Infra.Repositories;

public abstract class BaseTest<T> where T : BaseEntity
{
  protected readonly Mock<IReadonlyRepository<T>> Repository;

  public BaseTest() => Repository = new();
}