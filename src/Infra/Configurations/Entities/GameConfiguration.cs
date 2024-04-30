using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
  public void Configure(EntityTypeBuilder<Game> builder)
  {
    builder.Property(g => g.ReleasedAt).IsRequired();
    builder.HasOne(g => g.Publisher).WithMany(p => p.Games).HasForeignKey(p => p.PublisherId);
    builder.HasMany(g => g.Images).WithOne(i => i.Game).HasForeignKey(i => i.GameId);
    builder.HasMany(g => g.Genres).WithMany(g => g.Games).UsingEntity<GameGenre>();
  }
}