using Microsoft.EntityFrameworkCore;

namespace MoviesApiData
{
    public class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }
        public MoviesContext(DbContextOptions<MoviesContext> options)
          : base(options)
        {
        }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MyMovies> MyMovies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                const string connectionString =
          @"Server=.\SQLEXPRESS;Database=MoviesDataBase;Trusted_Connection=True;";

                optionsBuilder.UseSqlServer(connectionString);
            }

        }
    }
}
