using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class NamedEntityConfiguration : IEntityTypeConfiguration<NamedEntity>
{
  public void Configure(EntityTypeBuilder<NamedEntity> builder)
    => builder.UseTpcMappingStrategy().Property(e => e.Name).IsRequired().HasMaxLength(Constants.DefaultMaxLengthOfString);
}