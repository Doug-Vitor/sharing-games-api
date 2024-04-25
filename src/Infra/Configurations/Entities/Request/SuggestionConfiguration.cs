using Core.Entities.Request;
using Infra.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
{
  public void Configure(EntityTypeBuilder<Suggestion> builder)
  {
    builder.ToTable(nameof(Suggestion));
    builder.Property(g => g.Title).IsRequired().HasMaxLength(Constants.DefaultMaxLengthOfString);
  }
}