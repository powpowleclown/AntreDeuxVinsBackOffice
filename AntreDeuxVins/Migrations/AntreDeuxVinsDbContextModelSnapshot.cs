﻿// <auto-generated />
using AntreDeuxVins.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AntreDeuxVins.Migrations
{
    [DbContext(typeof(AntreDeuxVinsDbContext))]
    partial class AntreDeuxVinsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AntreDeuxVinsModel.Aliment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Aliments");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Cave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<Guid>("UtilisateurId");

                    b.HasKey("Id");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Caves");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Couleur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Couleurs");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Pays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Pays");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<int>("PaysId");

                    b.HasKey("Id");

                    b.HasIndex("PaysId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Utilisateur", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.Property<Guid?>("RoleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Vin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaveId");

                    b.Property<int?>("CouleurId");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<DateTime>("Millesime");

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<int?>("PaysId");

                    b.Property<int>("Quantite");

                    b.Property<int?>("RegionId");

                    b.Property<string>("Type");

                    b.Property<int>("Volume");

                    b.HasKey("Id");

                    b.HasIndex("CaveId");

                    b.HasIndex("CouleurId");

                    b.HasIndex("PaysId");

                    b.HasIndex("RegionId");

                    b.ToTable("Vins");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.VinAliment", b =>
                {
                    b.Property<int>("VinId");

                    b.Property<int>("AlimentId");

                    b.HasKey("VinId", "AlimentId");

                    b.HasIndex("AlimentId");

                    b.ToTable("VinAlments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Cave", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Region", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Pays", "Pays")
                        .WithMany("Regions")
                        .HasForeignKey("PaysId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Utilisateur", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Role", "Role")
                        .WithMany("Utilisateurs")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.Vin", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Cave", "Cave")
                        .WithMany("Vins")
                        .HasForeignKey("CaveId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntreDeuxVinsModel.Couleur", "Couleur")
                        .WithMany("Vins")
                        .HasForeignKey("CouleurId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AntreDeuxVinsModel.Pays", "Pays")
                        .WithMany()
                        .HasForeignKey("PaysId");

                    b.HasOne("AntreDeuxVinsModel.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId");
                });

            modelBuilder.Entity("AntreDeuxVinsModel.VinAliment", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Aliment", "Aliment")
                        .WithMany("AlimentVins")
                        .HasForeignKey("AlimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntreDeuxVinsModel.Vin", "Vin")
                        .WithMany("VinAliments")
                        .HasForeignKey("VinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Utilisateur")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Utilisateur")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntreDeuxVinsModel.Utilisateur")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("AntreDeuxVinsModel.Utilisateur")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
