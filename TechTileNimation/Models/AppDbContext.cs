using Microsoft.EntityFrameworkCore;

namespace TechTileNimation.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<SensationEntry> SensationEntry { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensationEntry>()
                .ToTable("sense_entry")
                .HasKey(s => s.Id)
                .HasName("se_id");

            modelBuilder.Entity<SensationEntry>()
                .Property(s => s.Id)
                .HasColumnName("se_id")
                .HasColumnType("INT");

            modelBuilder.Entity<SensationEntry>()
                .Property(s => s.Name)
                .HasColumnName("se_name")
                .HasColumnType("VARCHAR(50)");

            modelBuilder.Entity<SensationEntry>()
                .Property(s => s.PreviewImageLink)
                .HasColumnName("se_image_link")
                .HasColumnType("VARCHAR(150)");

            modelBuilder.Entity<SensationEntry>()
                .Property(s => s.SensationSoundLink)
                .HasColumnName("se_sound_link")
                .HasColumnType("VARCHAR(150)");

            modelBuilder.Entity<SensationEntry>()
                .Property(s => s.AnimationLink)
                .HasColumnName("se_animation_link")
                .HasColumnType("VARCHAR(150)");
        }
    }
}
