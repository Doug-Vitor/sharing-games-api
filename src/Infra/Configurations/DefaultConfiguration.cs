using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class DefaultConfiguration : IEntityTypeConfiguration<BaseEntity>
{
  public void Configure(EntityTypeBuilder<BaseEntity> builder)
  {
    builder.UseTpcMappingStrategy();
    builder.HasKey(prop => prop.Id);
    builder.Property(prop => prop.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    builder.Property(prop => prop.CreatedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
  }
}