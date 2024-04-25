using Core.Entities.Request;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class BaseRequestConfiguration : IEntityTypeConfiguration<BaseRequest>
{
  public void Configure(EntityTypeBuilder<BaseRequest> builder)
    => builder.UseTpcMappingStrategy()
              .Property(g => g.Title)
              .IsRequired()
              .HasMaxLength(Constants.DefaultMaxLengthOfString);
}