﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recepti.Data;

#nullable disable

namespace Recepti.Migrations
{
    [DbContext(typeof(ReceptiContext))]
    [Migration("20230526171311_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Recepti.Models.KorisnikRecepti", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ReceptId")
                        .HasColumnType("int");

                    b.Property<int>("ReceptiId")
                        .HasColumnType("int");

                    b.Property<string>("korisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReceptId");

                    b.ToTable("KorisnikRecepti");
                });

            modelBuilder.Entity("Recepti.Models.Recenzija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Komentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Korisnik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Ocena")
                        .HasColumnType("int");

                    b.Property<int>("ReceptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceptId");

                    b.ToTable("Recenzija");
                });

            modelBuilder.Entity("Recepti.Models.Recept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Kreator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naslov")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tekst")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Vegan")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Vreme")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Za_deca")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Recept");
                });

            modelBuilder.Entity("Recepti.Models.KorisnikRecepti", b =>
                {
                    b.HasOne("Recepti.Models.Recept", "Recept")
                        .WithMany()
                        .HasForeignKey("ReceptId");

                    b.Navigation("Recept");
                });

            modelBuilder.Entity("Recepti.Models.Recenzija", b =>
                {
                    b.HasOne("Recepti.Models.Recept", "Recept")
                        .WithMany("Recenzii")
                        .HasForeignKey("ReceptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recept");
                });

            modelBuilder.Entity("Recepti.Models.Recept", b =>
                {
                    b.Navigation("Recenzii");
                });
#pragma warning restore 612, 618
        }
    }
}
