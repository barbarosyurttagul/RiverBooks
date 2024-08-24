﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RiverBooks.Books;

#nullable disable

namespace RiverBooks.Books.Data.Migrations
{
    [DbContext(typeof(BookDbContext))]
    partial class BookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Books")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RiverBooks.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Books", "Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("28bf24de-6cc7-4fc0-87c4-b6547ae6fef5"),
                            Author = "J.R.R Tolkien",
                            Price = 9.99m,
                            Title = "The Fellowship of the Ring"
                        },
                        new
                        {
                            Id = new Guid("0ab9b3de-a34e-4114-9825-3704f9b716b3"),
                            Author = "J.R.R Tolkien",
                            Price = 10.99m,
                            Title = "The Two Towers"
                        },
                        new
                        {
                            Id = new Guid("e7bdd288-b0e2-41e1-8c5d-b76ce2a3335d"),
                            Author = "J.R.R Tolkien",
                            Price = 12.99m,
                            Title = "The Return of the King"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
