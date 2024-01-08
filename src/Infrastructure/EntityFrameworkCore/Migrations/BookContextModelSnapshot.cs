﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhiteBear.Infrastructure.EFCore;

#nullable disable

namespace WhiteBear.Infrastructure.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(BookContext))]
    partial class BookContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookAuthors", b =>
                {
                    b.Property<int>("AuthorsRecordId")
                        .HasColumnType("int");

                    b.Property<int>("BooksRecordId")
                        .HasColumnType("int");

                    b.HasKey("AuthorsRecordId", "BooksRecordId");

                    b.HasIndex("BooksRecordId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("WhiteBear.Infrastructure.EFCore.DbEntities.AuthorDbEntity", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecordId"));

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecordId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("WhiteBear.Infrastructure.EFCore.DbEntities.BookDbEntity", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecordId"));

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IsbnNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecordId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookAuthors", b =>
                {
                    b.HasOne("WhiteBear.Infrastructure.EFCore.DbEntities.AuthorDbEntity", null)
                        .WithMany()
                        .HasForeignKey("AuthorsRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhiteBear.Infrastructure.EFCore.DbEntities.BookDbEntity", null)
                        .WithMany()
                        .HasForeignKey("BooksRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
