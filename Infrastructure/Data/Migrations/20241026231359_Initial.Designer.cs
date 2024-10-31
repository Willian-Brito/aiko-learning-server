﻿// <auto-generated />
using System;
using AikoLearning.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241026231359_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("BYTEA")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text")
                        .HasColumnName("deleted_by");

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

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

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

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text")
                        .HasColumnName("deleted_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("ID");

                    b.HasIndex("ParentId");

                    b.ToTable("categories", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Back-End"
                        },
                        new
                        {
                            ID = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Front-End"
                        },
                        new
                        {
                            ID = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Mobile"
                        },
                        new
                        {
                            ID = 4,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Linguagem",
                            ParentId = 1
                        },
                        new
                        {
                            ID = 5,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Banco de Dados",
                            ParentId = 1
                        },
                        new
                        {
                            ID = 6,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Linguagem",
                            ParentId = 1
                        },
                        new
                        {
                            ID = 7,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Lógica de Programação",
                            ParentId = 1
                        },
                        new
                        {
                            ID = 8,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Design Patterns",
                            ParentId = 1
                        },
                        new
                        {
                            ID = 9,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Testes Automátizados",
                            ParentId = 1
                        },
                        new
                        {
                            ID = 10,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Arquitetura",
                            ParentId = 1
                        },
                        new
                        {
                            ID = 11,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Html",
                            ParentId = 2
                        },
                        new
                        {
                            ID = 12,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Css",
                            ParentId = 2
                        },
                        new
                        {
                            ID = 13,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Javascript",
                            ParentId = 2
                        },
                        new
                        {
                            ID = 14,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Frameworks",
                            ParentId = 2
                        },
                        new
                        {
                            ID = 15,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Nativo",
                            ParentId = 3
                        },
                        new
                        {
                            ID = 16,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Name = "Híbrido",
                            ParentId = 3
                        });
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.UserTokens", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("access_token");

                    b.Property<DateTime>("AccessTokenExpiration")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("access_token_expiration");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("RefreshTokenExpiration")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("refresh_token_expiration");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text")
                        .HasColumnName("deleted_by");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int[]>("Roles")
                        .HasColumnType("integer[]")
                        .HasColumnName("roles");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("ID");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = "1",
                            Email = "wbrito@aiko.digital",
                            Name = "Willian Brito",
                            Password = "$2a$11$R2rPEl2L7dEOo7fjUVA4CeySrz/a03JmNhJCglJRHnRlYzD8RRtFK",
                            Roles = new[] { 0 }
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

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.UserTokens", b =>
                {
                    b.HasOne("AikoLearning.Core.Domain.Model.Users", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Categories", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("AikoLearning.Core.Domain.Model.Users", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}