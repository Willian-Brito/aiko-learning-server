using AikoLearning.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AikoLearning.Infrastructure.Data.Configurations;

public class PersistenceCategoryConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.ID).ValueGeneratedOnAdd();
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.ParentId).IsRequired(false);
        builder
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .HasColumnType("timestamp without time zone")
            .IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnType("timestamp without time zone");
        builder.Property(c => c.DeletedAt).HasColumnType("timestamp without time zone");

        builder
            .HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Categories(1, "Back-End", null, "1"),
            new Categories(2, "Front-End", null, "1"),
            new Categories(3, "Mobile", null, "1"),

            // # Back-End
            new Categories(4, "Linguagem", 1, "1"),
            new Categories(5, "Banco de Dados", 1, "1"),
            new Categories(6, "Linguagem", 1, "1"),
            new Categories(7, "Lógica de Programação", 1, "1"),
            new Categories(8, "Design Patterns", 1, "1"),
            new Categories(9, "Testes Automátizados", 1, "1"),
            new Categories(10, "Arquitetura", 1, "1"),

            // # Front-End
            new Categories(11, "Html", 2, "1"),
            new Categories(12, "Css", 2, "1"),
            new Categories(13, "Javascript", 2, "1"),
            new Categories(14, "Frameworks", 2, "1"),

            // # Mobile
            new Categories(15, "Nativo", 3, "1"),
            new Categories(16, "Híbrido", 3, "1")
        );
    }
}

