using AikoLearning.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AikoLearning.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.ID).ValueGeneratedOnAdd();
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.ParentId).IsRequired(false);
        // builder.Property(c => c.CreatedAt).HasColumnType("timestamp without time zone").IsRequired();
        // builder.Property(c => c.UpdatedAt).HasColumnType("timestamp without time zone");
        // builder.Property(c => c.DeletedAt).HasColumnType("timestamp without time zone");

        builder
            .HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Categories(1, "Material Escolar", null),
            new Categories(2, "Eletrônicos", null),
            new Categories(3, "Acessórios", null)
        );
    }
}

