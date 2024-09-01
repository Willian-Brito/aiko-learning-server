using AikoLearning.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AikoLearning.Infrastructure.Data.Configurations;

public class PersistenceArticleConfiguration : IEntityTypeConfiguration<Articles>
{
    public void Configure(EntityTypeBuilder<Articles> builder)
    {
        builder.ToTable("articles");
        builder.HasKey(a => a.ID);
        builder.Property(a => a.ID).ValueGeneratedOnAdd();
        builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
        builder.Property(a => a.CategoryId).IsRequired();
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.Description).HasMaxLength(1000).IsRequired(false);
        builder.Property(a => a.ImageUrl).HasMaxLength(200).IsRequired(false);
        builder.Property(a => a.Content).HasColumnType("BYTEA").IsRequired(false);
        // builder.Property(a => a.CreatedAt).HasColumnType("timestamp without time zone");
        // builder.Property(a => a.UpdatedAt).HasColumnType("timestamp without time zone");
        // builder.Property(a => a.DeletedAt).HasColumnType("timestamp without time zone");

        builder
            .HasOne(a => a.Category)
            .WithMany(a => a.Articles)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}