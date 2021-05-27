using BlazorPeliculas.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        public List<Pelicula> ObtenerPeliculas()
        {
           return  new List<Pelicula>()
        {
                new Pelicula(){Titulo = "Spider-Man: Far From Home", 
                    Lanzamiento  = new DateTime(2019, 7, 2), 
                    Poster= "https://pics.filmaffinity.com/spider_man_spiderman-945131720-mmed.jpg"},
                new Pelicula(){Titulo = "Moana", Lanzamiento  = new DateTime(2016, 11, 23), 
                    Poster = "https://pics.filmaffinity.com/moana-802273424-mmed.jpg"},
                new Pelicula(){Titulo = "Inception", Lanzamiento  = new DateTime(2010, 7, 16),
                    Poster = "https://pics.filmaffinity.com/inception-652954101-mmed.jpg"}
            };
        }
    }
}
