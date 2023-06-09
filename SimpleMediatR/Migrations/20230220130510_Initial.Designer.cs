﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleMediatr.Data.Database;

#nullable disable

namespace SimpleMediatR.Migrations
{
    [DbContext(typeof(SimpleMediatrDataContext))]
    [Migration("20230220130510_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SimpleMediatR.Models.Category", b =>
                {
                    b.Property<long>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1L,
                            Name = "Watersports"
                        },
                        new
                        {
                            CategoryId = 2L,
                            Name = "Football"
                        },
                        new
                        {
                            CategoryId = 3L,
                            Name = "Chess"
                        });
                });

            modelBuilder.Entity("SimpleMediatR.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"));

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1L,
                            CategoryId = 1L,
                            Name = "Kayak",
                            Price = 275m
                        },
                        new
                        {
                            ProductId = 2L,
                            CategoryId = 1L,
                            Name = "Lifejacket",
                            Price = 48.95m
                        },
                        new
                        {
                            ProductId = 3L,
                            CategoryId = 2L,
                            Name = "Ball",
                            Price = 19.50m
                        },
                        new
                        {
                            ProductId = 4L,
                            CategoryId = 2L,
                            Name = "Corner Flags",
                            Price = 34.95m
                        },
                        new
                        {
                            ProductId = 5L,
                            CategoryId = 2L,
                            Name = "Stadium",
                            Price = 79500m
                        },
                        new
                        {
                            ProductId = 6L,
                            CategoryId = 3L,
                            Name = "Thinking Cap",
                            Price = 16m
                        },
                        new
                        {
                            ProductId = 7L,
                            CategoryId = 3L,
                            Name = "Unsteady Chair",
                            Price = 29.95m
                        },
                        new
                        {
                            ProductId = 8L,
                            CategoryId = 3L,
                            Name = "Human Chess Board",
                            Price = 75m
                        },
                        new
                        {
                            ProductId = 9L,
                            CategoryId = 3L,
                            Name = "T-shirt",
                            Price = 1200m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
