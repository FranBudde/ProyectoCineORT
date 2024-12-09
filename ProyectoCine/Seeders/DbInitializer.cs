using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoCine.Models;

public static class DbInitializer
{
    public static void Initialize(CineContext context)
    {
        Console.WriteLine("Iniciando el proceso de inicialización de la base de datos...");

        // Asegurarse de que la base de datos esté creada
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Base de datos creada.");
        }
        else
        {
            Console.WriteLine("La base de datos ya existía.");
        }

        // Poblar la tabla Genero
        if (!context.Generos.Any())
        {
            Console.WriteLine("Poblando la tabla Genero...");
            var generos = new List<Genero>
            {
                new Genero { TipoGenero = "Drama" },
                new Genero { TipoGenero = "Comedia" },
                new Genero { TipoGenero = "Acción" }
            };
            context.Generos.AddRange(generos);
            context.SaveChanges();
            Console.WriteLine("Tabla Genero poblada.");
        }
        else
        {
            Console.WriteLine("La tabla Genero ya estaba poblada.");
        }

        // Poblar la tabla Sala
        if (!context.Salas.Any())
        {
            Console.WriteLine("Poblando la tabla Sala...");
            var salas = new List<Sala>
            {
                new Sala { Capacidad = 100 },
                new Sala { Capacidad = 150 },
                new Sala { Capacidad = 200 }
            };
            context.Salas.AddRange(salas);
            context.SaveChanges();
            Console.WriteLine("Tabla Sala poblada.");
        }
        else
        {
            Console.WriteLine("La tabla Sala ya estaba poblada.");
        }

        // Poblar la tabla Pelicula
        if (!context.Peliculas.Any())
        {
            Console.WriteLine("Poblando la tabla Pelicula...");
            var peliculas = new List<Pelicula>
            {
                new Pelicula
                {
                    NamePelicula = "El Padrino",
                    Duracion = 175,
                    Idioma = "Español",
                    Subtitulada = true,
                    IdGenero = context.Generos.First().IdGenero, // Referencia a un IdGenero existente
                    IdSala = context.Salas.First().IdSala // Referencia a un IdSala existente
                },
                new Pelicula
                {
                    NamePelicula = "Titanic",
                    Duracion = 195,
                    Idioma = "Inglés",
                    Subtitulada = true,
                    IdGenero = context.Generos.Skip(1).First().IdGenero, // Segundo género
                    IdSala = context.Salas.Skip(1).First().IdSala // Segunda sala
                }
            };
            context.Peliculas.AddRange(peliculas);
            context.SaveChanges();
            Console.WriteLine("Tabla Pelicula poblada.");
        }
        else
        {
            Console.WriteLine("La tabla Pelicula ya estaba poblada.");
        }

        // Poblar la tabla Horario
        if (!context.Horarios.Any())
        {
            Console.WriteLine("Poblando la tabla Horario...");
            var horarios = new List<Horario>
    {
        new Horario { Fecha = new DateTime(2024, 12, 9), Hora = new TimeOnly(18, 30) },
        new Horario { Fecha = new DateTime(2024, 12, 10), Hora = new TimeOnly(20, 0) }
    };
            context.Horarios.AddRange(horarios);
            context.SaveChanges();
            Console.WriteLine("Tabla Horario poblada.");
        }
        else
        {
            Console.WriteLine("La tabla Horario ya estaba poblada.");
        }

        // Poblar la tabla PeliculaHorario
        if (!context.PeliculaHorarios.Any())
        {
            Console.WriteLine("Poblando la tabla PeliculaHorario...");
            var peliculaHorarios = new List<PeliculaHorario>
            {
                new PeliculaHorario
                {
                    IdPelicula = context.Peliculas.First().IdPelicula,
                    IdHorario = context.Horarios.First().IdHorario
                }
            };
            context.PeliculaHorarios.AddRange(peliculaHorarios);
            context.SaveChanges();
            Console.WriteLine("Tabla PeliculaHorario poblada.");
        }
        else
        {
            Console.WriteLine("La tabla PeliculaHorario ya estaba poblada.");
        }

        // Poblar la tabla Butaca
        if (!context.Butacas.Any())
        {
            Console.WriteLine("Poblando la tabla Butaca...");
            var butacas = new List<Butaca>
            {
                new Butaca { numeroButaca = 1, Letra = "A", IdSala = context.Salas.First().IdSala },
                new Butaca { numeroButaca = 2, Letra = "B", IdSala = context.Salas.First().IdSala },
                new Butaca { numeroButaca = 1, Letra = "A", IdSala = context.Salas.Skip(1).First().IdSala }
            };
            context.Butacas.AddRange(butacas);
            context.SaveChanges();
            Console.WriteLine("Tabla Butaca poblada.");
        }
        else
        {
            Console.WriteLine("La tabla Butaca ya estaba poblada.");
        }

        // Poblar la tabla PeliculaGenero
        if (!context.PeliculaGeneros.Any())
        {
            Console.WriteLine("Poblando la tabla PeliculaGenero...");
            var peliculaGeneros = new List<PeliculaGenero>
            {
                new PeliculaGenero
                {
                    IdPelicula = context.Peliculas.First().IdPelicula,
                    IdGenero = context.Generos.First().IdGenero
                }
            };
            context.PeliculaGeneros.AddRange(peliculaGeneros);
            context.SaveChanges();
            Console.WriteLine("Tabla PeliculaGenero poblada.");
        }
        else
        {
            Console.WriteLine("La tabla PeliculaGenero ya estaba poblada.");
        }
    }
}
