using Microsoft.EntityFrameworkCore;

namespace ProyectoCine.Models
{
    public class CineContext : DbContext
    {
        public CineContext(DbContextOptions<CineContext> options)
            : base(options)
        {
        }

        public DbSet<Butaca> Butacas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<PeliculaGenero> PeliculaGeneros { get; set; }
        public DbSet<PeliculaHorario> PeliculaHorarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar valor predeterminado para IsAvailable en Butaca
            modelBuilder.Entity<Butaca>()
                .Property(b => b.IsAvailable)
                .HasDefaultValue(true);

            // Configuración de relaciones para Reservas
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Butaca)
                .WithMany()
                .HasForeignKey(r => r.IdButaca)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.PeliculaHorario)
                .WithMany()
                .HasForeignKey(r => r.IdPeliculaHorario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de relaciones para PeliculaGeneros
            modelBuilder.Entity<PeliculaGenero>()
                .HasOne(pg => pg.Pelicula)
                .WithMany(p => p.PeliculaGeneros)
                .HasForeignKey(pg => pg.IdPelicula)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PeliculaGenero>()
                .HasOne(pg => pg.Genero)
                .WithMany(g => g.PeliculaGeneros)
                .HasForeignKey(pg => pg.IdGenero)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
