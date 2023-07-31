﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RockPaperScissors.Domain.Db;

#nullable disable

namespace RockPaperScissors.Domain.Migrations
{
    [DbContext(typeof(RockPaperScissorsDbContext))]
    [Migration("20230731203201_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RockPaperScissors.Domain.Game.Game", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("text");

                    b.Property<int>("CurrentRound")
                        .HasColumnType("integer");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean");

                    b.HasKey("GameId");

                    b.ToTable("Game");
                });
#pragma warning restore 612, 618
        }
    }
}
