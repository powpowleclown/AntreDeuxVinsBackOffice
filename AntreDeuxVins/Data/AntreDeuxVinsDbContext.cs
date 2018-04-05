using AntreDeuxVins.Providers;
using AntreDeuxVinsModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AntreDeuxVins.Data
{
    public class AntreDeuxVinsDbContext : DbContext
    {
        public DbSet<Aliment> Aliments { get; set; }
        public DbSet<Cave> Caves { get; set; }
        public DbSet<CaveVin> CaveVins { get; set; }
        public DbSet<Couleur> Couleurs { get; set; }
        public DbSet<Pays> Pays { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Vin> Vins { get; set; }
        public DbSet<VinAliment> VinAlments { get; set; }

    public AntreDeuxVinsDbContext(DbContextOptions<AntreDeuxVinsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Cave>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<CaveVin>()
                .HasKey(cv => new { cv.CaveId, cv.VinId });
            modelBuilder.Entity<CaveVin>()
                .HasOne(cv => cv.Cave)
                .WithMany(c => c.CaveVins)
                .HasForeignKey(cv => cv.CaveId);
            modelBuilder.Entity<CaveVin>()
                .HasOne(cv => cv.Vin)
                .WithMany(v => v.VinCaves)
                .HasForeignKey(cv => cv.VinId);
            modelBuilder.Entity<VinAliment>()
               .HasKey(va => new { va.VinId, va.AlimentId});
            modelBuilder.Entity<VinAliment>()
                .HasOne(va => va.Vin)
                .WithMany(v => v.VinAliments)
                .HasForeignKey(va => va.VinId);
            modelBuilder.Entity<VinAliment>()
                .HasOne(va => va.Aliment)
                .WithMany(a => a.AlimentVins)
                .HasForeignKey(va => va.AlimentId);
            modelBuilder.Entity<Couleur>()
                .HasMany(c => c.Vins)
                .WithOne(v => v.Couleur);
            modelBuilder.Entity<Pays>()
                .HasMany(p => p.Regions)
                .WithOne(r => r.Pays);
            modelBuilder.Entity<Region>()
                .HasOne(r => r.Pays)
                .WithMany(p => p.Regions);
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
