﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesApiData;

namespace MoviesApiData.Migrations
{
    [DbContext(typeof(MoviesContext))]
    partial class MoviesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoviesApiData.Movies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IMDB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWatched")
                        .HasColumnType("bit");

                    b.Property<int?>("MyMoviesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisteredInDataBase")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MyMoviesId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MoviesApiData.MyMovies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("IMDB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRate")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWatched")
                        .HasColumnType("bit");

                    b.Property<double?>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MyMovies");
                });

            modelBuilder.Entity("MoviesApiData.Movies", b =>
                {
                    b.HasOne("MoviesApiData.MyMovies", "MyMovies")
                        .WithMany("Movies")
                        .HasForeignKey("MyMoviesId");
                });
#pragma warning restore 612, 618
        }
    }
}
