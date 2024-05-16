using Core.Entities.Request;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class GameRequestConfiguration : IEntityTypeConfiguration<GameRequest>
{
  public void Configure(EntityTypeBuilder<GameRequest> builder)
  {
    builder.HasOne(typeof(User)).WithMany(nameof(User.GameRequests)).HasForeignKey(nameof(GameRequest.UserId));
    builder.HasOne(g => g.Game).WithMany().HasForeignKey(g => g.GameId).IsRequired(false);
    builder.Property(g => g.GameUrl).IsRequired();
  }
}