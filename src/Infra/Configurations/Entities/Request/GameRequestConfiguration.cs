using Core.Entities.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class GameRequestConfiguration : IEntityTypeConfiguration<GameRequest>
{
  public void Configure(EntityTypeBuilder<GameRequest> builder)
  {
    builder.ToTable(nameof(GameRequest));
    builder.Property(g => g.GameUrl).IsRequired();
  }
}