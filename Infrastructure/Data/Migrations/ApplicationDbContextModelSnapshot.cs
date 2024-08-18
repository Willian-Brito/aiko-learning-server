﻿// <auto-generated />
using System;
using AikoLearning.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Articles", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<byte[]>("Content")
                        .HasColumnType("bytea")
                        .HasColumnName("content");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("articles", (string)null);
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Categories", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.HasKey("ID");

                    b.HasIndex("ParentId");

                    b.ToTable("categories", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Material Escolar"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Eletrônicos"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Acessórios"
                        });
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_admin");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("ID");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "wbrito@aiko.digital",
                            IsAdmin = true,
                            Name = "Willian Brito",
                            Password = "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK"
                        });
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Articles", b =>
                {
                    b.HasOne("AikoLearning.Core.Domain.Model.Categories", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AikoLearning.Core.Domain.Model.Users", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Categories", b =>
                {
                    b.HasOne("AikoLearning.Core.Domain.Model.Categories", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Categories", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Users", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}