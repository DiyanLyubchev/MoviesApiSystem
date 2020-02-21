using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesApiData
{
    public class MyMovies
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string IMDB { get; set; }

        public int Year { get; set; }

        public bool IsWatched { get; set; }

        public bool IsRate { get; set; }

        public double? Rate { get; set; }

        public DateTime AddOn { get; set; }

        public ICollection<Movies> Movies { get; set; }
    }
}
