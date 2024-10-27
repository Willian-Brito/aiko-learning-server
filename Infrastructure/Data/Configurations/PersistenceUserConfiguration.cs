using System.Runtime.Serialization;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AikoLearning.Infrastructure.Data.Configurations;

public class PersistenceUserConfiguration : IEntityTypeConfiguration<Users>
{    
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("users");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.ID).ValueGeneratedOnAdd();
        builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Password).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(u => u.Roles).HasColumnType("integer[]").IsRequired(false);
        builder
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .HasColumnType("timestamp without time zone")
            .IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnType("timestamp without time zone");
        builder.Property(c => c.DeletedAt).HasColumnType("timestamp without time zone");        
        
        builder.HasData(
            new Users
            (
                id: 1,
                name: "Willian Brito",
                password: "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK", 
                email: "wbrito@aiko.digital",
                roles: new List<Role> {Role.Administrator},
                createdBy: "1"
            )
        );
    }
}