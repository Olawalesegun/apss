using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using APSS.Domain.Entities;

namespace APSS.Infrastructure.Repositories.EntityFramework.Configuration;

public sealed class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // relationships
        builder
            .HasMany(u => u.Accounts)
            .WithOne(a => a.User)
            .IsRequired();
    }
}