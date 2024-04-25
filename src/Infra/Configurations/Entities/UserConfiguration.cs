using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasMany(u => u.Suggestions).WithOne().HasForeignKey(s => s.UserId);
    builder.HasMany(u => u.GameRequests).WithOne().HasForeignKey(s => s.UserId);
  }
}