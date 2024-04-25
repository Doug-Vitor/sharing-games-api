using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class ImageConfiguration : IEntityTypeConfiguration<Image>
{
  public void Configure(EntityTypeBuilder<Image> builder)
  {
    builder.Property(i => i.IsCoverPhoto).HasDefaultValue(false);
    builder.Property(i => i.Url).IsRequired();
    builder.Property(i => i.FileName).IsRequired();
    builder.HasOne(i => i.Game).WithMany(g => g.Images).HasForeignKey(i => i.GameId);
  }
}