using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApiData
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string IMDB { get; set; }

        public int Year { get; set; }

        public DateTime RegisteredInDataBase { get; set; }

        public bool IsWatched { get; set; }

        public int? MyMoviesId { get; set; }

        public MyMovies MyMovies { get; set; }

    }
}
