using System;

namespace MoviesApi.Models
{
    public class MyMoviesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string IMDB { get; set; }

        public int Year { get; set; }

        public DateTime AddOn { get; set; }
    }
}
