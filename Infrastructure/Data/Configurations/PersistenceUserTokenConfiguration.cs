using AikoLearning.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AikoLearning.Infrastructure.Data.Configurations;

public class PersistenceUserTokenConfiguration : IEntityTypeConfiguration<UserTokens>
{    
    public void Configure(EntityTypeBuilder<UserTokens> builder)
    {
        builder.ToTable("user_tokens");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.ID).ValueGeneratedOnAdd();
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.AccessToken).HasMaxLength(1000).IsRequired();
        builder.Property(c => c.RefreshToken).HasMaxLength(1000).IsRequired();
        builder.Property(c => c.AccessTokenExpiration).HasColumnType("timestamp without time zone").IsRequired();
        builder.Property(c => c.RefreshTokenExpiration).HasColumnType("timestamp without time zone").IsRequired();

        builder
            .HasOne(a => a.User)
            .WithMany(a => a.UserTokens)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}