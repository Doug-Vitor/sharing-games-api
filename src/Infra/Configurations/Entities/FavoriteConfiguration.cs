using Core.Entities;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
  public void Configure(EntityTypeBuilder<Favorite> builder)
  {
    builder.HasOne(typeof(User)).WithMany(nameof(User.Favorites)).HasForeignKey(nameof(Favorite.UserId));
    builder.HasOne(f => f.Game).WithMany(g => g.Favorites).HasForeignKey(f => f.GameId);
  }
}