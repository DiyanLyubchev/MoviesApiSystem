using System;

namespace MoviesApiService
{
    public class MoviesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string IMDB { get; set; }

        public int Year { get; set; }

        public bool IsWatched { get; set; }

        public DateTime RegisteredInDataBase { get; set; }

    }
}
