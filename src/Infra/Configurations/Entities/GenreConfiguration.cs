using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
  public void Configure(EntityTypeBuilder<Genre> builder)
    => builder.HasMany(g => g.Games).WithMany(g => g.Genres).UsingEntity<GameGenre>();
}