namespace PruebaTecnicaScarletteGalo.Data
{
    using Microsoft.EntityFrameworkCore;
    using PruebaTecnicaScarletteGalo.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas (DbSet)
        public DbSet<User> Users { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Password)
                      .IsRequired();

                entity.Property(u => u.Role)
                      .IsRequired();

                entity.HasOne(u => u.State)
                      .WithMany(s => s.Users)
                      .HasForeignKey(u => u.StateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Space
            modelBuilder.Entity<Space>(entity =>
            {
                entity.HasKey(s => s.SpaceId);

                entity.Property(s => s.SpaceName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.Capacity)
                      .IsRequired();

                entity.HasOne(s => s.State)
                      .WithMany(st => st.Spaces)
                      .HasForeignKey(s => s.StateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.ReservationId);

                entity.Property(r => r.Date)
                      .IsRequired();

                entity.Property(r => r.StartTime)
                      .IsRequired();

                entity.Property(r => r.EndTime)
                      .IsRequired();

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reservations)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Space)
                      .WithMany(s => s.Reservations)
                      .HasForeignKey(r => r.SpaceId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.State)
                      .WithMany(s => s.Reservations)
                      .HasForeignKey(r => r.StateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de State
            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(st => st.StateId);

                entity.Property(st => st.StateName)
                      .IsRequired()
                      .HasMaxLength(50);
            });
        }
    }

}
