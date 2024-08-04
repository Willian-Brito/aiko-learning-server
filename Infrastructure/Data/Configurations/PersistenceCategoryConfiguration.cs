using AikoLearning.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AikoLearning.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.ParentId).IsRequired(false);
        builder.Property(c => c.CreatedAt).HasColumnType("timestamp without time zone");
        builder.Property(c => c.UpdatedAt).HasColumnType("timestamp without time zone");
        builder.Property(c => c.DeletedAt).HasColumnType("timestamp without time zone");

        builder
            .HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Category(1, "Material Escolar", null),
            new Category(2, "Eletrônicos", null),
            new Category(3, "Acessórios", null)
        );
    }
}

