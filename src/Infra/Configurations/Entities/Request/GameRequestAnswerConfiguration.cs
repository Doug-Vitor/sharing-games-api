using Core.Enums;
using Core.Entities.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class GameRequestAnswerConfiguration : IEntityTypeConfiguration<GameRequestAnswer>
{
  public void Configure(EntityTypeBuilder<GameRequestAnswer> builder)
  {
    builder.Property(g => g.Status)
           .IsRequired()
           .HasConversion<string>()
           .HasDefaultValue(GameRequestAnswerStatus.Pending);
  }
}