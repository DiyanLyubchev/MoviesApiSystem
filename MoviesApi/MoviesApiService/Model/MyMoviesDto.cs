using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApiService.Model
{
    public class MyMoviesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string IMDB { get; set; }

        public int Year { get; set; }

        public DateTime AddOn { get; set; }

        public bool IsWatched { get; set; }

        public bool IsRate { get; set; }

        public double? Rate { get; set; }

    }
}
