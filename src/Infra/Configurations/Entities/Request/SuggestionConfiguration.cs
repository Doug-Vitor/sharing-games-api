using Core.Entities.Request;
using Infra.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
{
  public void Configure(EntityTypeBuilder<Suggestion> builder)
    => builder.HasOne(typeof(User)).WithMany(nameof(User.Suggestions)).HasForeignKey(nameof(Suggestion.UserId));
}