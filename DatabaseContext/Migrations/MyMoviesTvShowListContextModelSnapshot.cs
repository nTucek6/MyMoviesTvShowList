﻿// <auto-generated />
using System;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseContext.Migrations
{
    [DbContext(typeof(MyMoviesTvShowListContext))]
    partial class MyMoviesTvShowListContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entites.MoviesCrewEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CharacterName")
                        .HasColumnType("text");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<int>("PersonnelType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MoviesCrew");
                });

            modelBuilder.Entity("Entites.MoviesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTimeAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Duration")
                        .HasColumnType("numeric");

                    b.Property<string>("Genres")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("MovieImageData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Synopsis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Entites.UsersEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Joined")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("ProfileImageData")
                        .HasColumnType("bytea");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserBio")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
