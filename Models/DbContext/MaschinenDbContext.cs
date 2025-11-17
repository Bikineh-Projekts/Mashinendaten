using Microsoft.EntityFrameworkCore;
using MaschinenDataein.Models.Data;
using MaschinenDataein.Models;
using Microsoft.EntityFrameworkCore.Design;

public class MaschinenDbContextFactory : IDesignTimeDbContextFactory<MaschinenDbContext>
{
    public MaschinenDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MaschinenDbContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

        return new MaschinenDbContext(optionsBuilder.Options);
    }
}

namespace MaschinenDataein.Models
{
    public class MaschinenDbContext : DbContext
    {
        public MaschinenDbContext(DbContextOptions<MaschinenDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Planungs> Planung { get; set; } = null!;          
        public DbSet<LeistungsDaten> Leistungsdaten { get; set; } = null!;
        public DbSet<TemperaturDaten> Temperaturdaten { get; set; } = null!;
        public DbSet<Maschinenprogrammen> MaschinenProgrammen { get; set; } = null!;
        public DbSet<AbzugsDaten> Abzugsdaten { get; set; } = null!;
        public DbSet<AlarmDaten> Alarmdaten { get; set; } = null!;
        public DbSet<ZustandsDaten> Zustandsdaten { get; set; } = null!;
        public DbSet<ZustandsMeldung> Zustandsmeldung { get; set; } = null!;
        public DbSet<StoerungsDaten> Stoerungsdaten { get; set; } = null!;
        public DbSet<StoerungsMeldung> Stoerungsmeldung { get; set; } = null!;
        public DbSet<Maschine> Maschinen { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Falls deine Entities eine Eigenschaft "Id" haben, sind die Keys per Konvention gesetzt
            // und dieser Block kann oft leer bleiben. Wenn du es explizit möchtest:
            modelBuilder.Entity<Planungs>().HasKey(e => e.Id);
            modelBuilder.Entity<LeistungsDaten>().HasKey(e => e.Id);
            modelBuilder.Entity<TemperaturDaten>().HasKey(e => e.Id);
            modelBuilder.Entity<Maschinenprogrammen>().HasKey(e => e.Id);
            modelBuilder.Entity<AbzugsDaten>().HasKey(e => e.Id);
            modelBuilder.Entity<AlarmDaten>().HasKey(e => e.Id);
            modelBuilder.Entity<ZustandsDaten>().HasKey(e => e.Id);
            modelBuilder.Entity<ZustandsMeldung>().HasKey(e => e.Id);
            modelBuilder.Entity<StoerungsDaten>().HasKey(e => e.Id);
            modelBuilder.Entity<StoerungsMeldung>().HasKey(e => e.Id);
            modelBuilder.Entity<Maschine>().HasKey(e => e.Id);

            // Beispiel-Beziehung (optional):
            // modelBuilder.Entity<Planung>()
            //     .HasOne<Maschine>()
            //     .WithMany()
            //     .HasForeignKey(p => p.MaschineID)
            //     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
