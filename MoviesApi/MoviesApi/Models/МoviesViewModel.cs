namespace MoviesApi.Models
{
    public class МoviesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string IMDB { get; set; }

        public int Year { get; set; }

        public bool IsWatched { get; set; }
    }
}
