using Microsoft.EntityFrameworkCore;

namespace ProyectoCine.Data
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

            // Aquí configuras las relaciones
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

            // ... cualquier otra configuración
        }
    }
}
