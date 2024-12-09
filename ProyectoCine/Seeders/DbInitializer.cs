using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoCine.Data;

public static class DbInitializer
{
    public static void Initialize(CineContext context)
    {
        // Asegurarse de que la base de datos esté creada
        context.Database.EnsureCreated();

        // Poblar la tabla Genero
        if (!context.Generos.Any())
        {
            var generos = new List<Genero>
            {
                new Genero { IdGenero = 1, TipoGenero = "Drama" },
                new Genero { IdGenero = 2, TipoGenero = "Comedia" },
                new Genero { IdGenero = 3, TipoGenero = "Acción" }
            };
            context.Generos.AddRange(generos);
            context.SaveChanges();
        }

        // Poblar la tabla Sala
        if (!context.Salas.Any())
        {
            var salas = new List<Sala>
            {
                new Sala { IdSala = 1, Capacidad = 100 },
                new Sala { IdSala = 2, Capacidad = 150 },
                new Sala { IdSala = 3, Capacidad = 200 }
            };
            context.Salas.AddRange(salas);
            context.SaveChanges();
        }

        // Poblar la tabla Pelicula
        if (!context.Peliculas.Any())
        {
            var peliculas = new List<Pelicula>
            {
                new Pelicula
                {
                    IdPelicula = 1,
                    NamePelicula = "El Padrino",
                    Duracion = 175,
                    Idioma = "Español",
                    Subtitulada = true,
                    IdGenero = 1, // Drama
                    IdSala = 1 // Sala 1
                },
                new Pelicula
                {
                    IdPelicula = 2,
                    NamePelicula = "Titanic",
                    Duracion = 195,
                    Idioma = "Inglés",
                    Subtitulada = true,
                    IdGenero = 2, // Comedia
                    IdSala = 2 // Sala 2
                }
            };
            context.Peliculas.AddRange(peliculas);
            context.SaveChanges();
        }

        // Poblar la tabla Horario
        if (!context.Horarios.Any())
        {
            var horarios = new List<Horario>
            {
                new Horario
                {
                    IdHorario = 1,
                    Fecha = new DateTime(2024, 12, 9),
                    Hora = TimeOnly.Parse("18:30")
                },
                new Horario
                {
                    IdHorario = 2,
                    Fecha = new DateTime(2024, 12, 10),
                    Hora = TimeOnly.Parse("20:00")
                }
            };
            context.Horarios.AddRange(horarios);
            context.SaveChanges();
        }

        // Poblar la tabla PeliculaHorario
        if (!context.PeliculaHorarios.Any())
        {
            var peliculaHorarios = new List<PeliculaHorario>
            {
                new PeliculaHorario
                {
                    IdPeliculaGenero = 1,
                    IdPelicula = 1,
                    IdHorario = 1
                }
            };
            context.PeliculaHorarios.AddRange(peliculaHorarios);
            context.SaveChanges();
        }

        // Poblar la tabla Butaca
        if (!context.Butacas.Any())
        {
            var butacas = new List<Butaca>
            {
                new Butaca { IdButaca = 1, numeroButaca = 1, Letra = "A", IdSala = 1 },
                new Butaca { IdButaca = 2, numeroButaca = 2, Letra = "B", IdSala = 1 },
                new Butaca { IdButaca = 3, numeroButaca = 1, Letra = "A", IdSala = 2 }
            };
            context.Butacas.AddRange(butacas);
            context.SaveChanges();
        }

        // Poblar la tabla PeliculaGenero
        if (!context.PeliculaGeneros.Any())
        {
            var peliculaGeneros = new List<PeliculaGenero>
            {
                new PeliculaGenero
                {
                    IdPeliculaGenero = 1,
                    IdPelicula = 1,
                    IdGenero = 1 // Drama
                }
            };
            context.PeliculaGeneros.AddRange(peliculaGeneros);
            context.SaveChanges();
        }
    }
}
