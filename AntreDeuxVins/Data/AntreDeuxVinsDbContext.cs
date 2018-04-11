using AntreDeuxVins.Providers;
using AntreDeuxVinsModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace AntreDeuxVins.Data
{
    public class AntreDeuxVinsDbContext : IdentityDbContext<Utilisateur,Role, Guid>
    {
        public DbSet<Aliment> Aliments { get; set; }
        public DbSet<Cave> Caves { get; set; }
        public DbSet<Couleur> Couleurs { get; set; }
        public DbSet<Pays> Pays { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Vin> Vins { get; set; }
        public DbSet<VinAliment> VinAlments { get; set; }

        public AntreDeuxVinsDbContext(DbContextOptions<AntreDeuxVinsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<VinAliment>()
               .HasKey(va => new { va.VinId, va.AlimentId});
            modelBuilder.Entity<VinAliment>()
                .HasOne(va => va.Vin)
                .WithMany(v => v.VinAliments)
                .HasForeignKey(va => va.VinId);
            modelBuilder.Entity<VinAliment>()
                .HasOne(va => va.Aliment)
                .WithMany(a => a.AlimentVins)
                .HasForeignKey(va => va.AlimentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Couleur>()
                .HasMany(c => c.Vins)
                .WithOne(v => v.Couleur)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Cave>()
                .HasMany(c => c.Vins)
                .WithOne(v => v.Cave)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Pays>()
                .HasMany(p => p.Regions)
                .WithOne(r => r.Pays)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Region>()
                .HasOne(r => r.Pays)
                .WithMany(p => p.Regions)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Utilisateurs)
                .WithOne(u => u.Role);
            modelBuilder.Entity<Utilisateur>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Utilisateurs);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var fact = new LoggerFactory();
            fact.AddProvider(new SQLLoggerProvider());
            optionsBuilder.UseLoggerFactory(fact);
        }


    }
}
