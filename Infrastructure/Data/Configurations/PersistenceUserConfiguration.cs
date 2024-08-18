using AikoLearning.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AikoLearning.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Users>
{    
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("users");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.ID).ValueGeneratedOnAdd();
        builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Password).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.IsAdmin).HasDefaultValue(false).IsRequired();
        // builder.Property(c => c.CreatedAt).HasColumnType("timestamp without time zone");
        // builder.Property(c => c.UpdatedAt).HasColumnType("timestamp without time zone");
        // builder.Property(c => c.DeletedAt).HasColumnType("timestamp without time zone");

        builder.HasData(
            new Users(
                1,
                "Willian Brito",
                "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                "wbrito@aiko.digital",
                true
            )
        );
    }
}