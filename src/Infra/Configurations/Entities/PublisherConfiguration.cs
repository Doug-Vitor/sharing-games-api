using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
  public void Configure(EntityTypeBuilder<Publisher> builder)
    => builder.HasMany(p => p.Games).WithOne(g => g.Publisher).HasForeignKey(g => g.PublisherId);
}