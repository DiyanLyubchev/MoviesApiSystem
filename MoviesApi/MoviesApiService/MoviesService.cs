using MoviesApiData;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using MoviesApiService.Model;
using System.Threading;

namespace MoviesApiService
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesContext context;
        private readonly IMapper mapper;
        public MoviesService(MoviesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task AddMovieToDataAsync(List<MoviesDto> dto , CancellationToken cancellationToken)
        {
            var listMovies = this.mapper.Map<List<Movies>>(dto);

            var existMovieInDatabase = await this.context.Movies
                .Select(title => title.Title)
                .ToListAsync();

            if (existMovieInDatabase.Count() == 0)
            {
                await this.context.Movies.AddRangeAsync(listMovies);
            }
            else
            {
                foreach (var movie in listMovies)
                {
                    if (!existMovieInDatabase.Contains(movie.Title))
                    {
                        await this.context.Movies.AddAsync(movie);
                    }
                }
            }

            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<MoviesDto> FindByIdAsync(int id )
        {
            var movie = await this.context.Movies
                .FindAsync(id);

            return this.mapper.Map<MoviesDto>(movie);
        }

        private MyMovies MyMoviesGenerator(MoviesDto dto)
        {
            Func<string, int, string, MyMovies> myMovieGenerator = (title, year, imdb) => new MyMovies
            {
                Title = title,
                Year = year,
                IMDB = imdb,
                AddOn = DateTime.Now
            };

            return myMovieGenerator(dto.Title, dto.Year, dto.IMDB);
        }

        public async Task AddToMyFavoriteAsync(MoviesDto dto , CancellationToken cancellationToken)
        {
            var myMovie = MyMoviesGenerator(dto);

            await this.context.MyMovies.AddAsync(myMovie);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<MyMoviesDto>> GetMyMoviesAsync(CancellationToken cancellationToken)
        {
            var movies = await this.context.MyMovies
                 .Where(reviewed => reviewed.IsWatched == false)
                .ToListAsync(cancellationToken);

            var moviesDto = this.mapper.Map<List<MyMoviesDto>>(movies);

            return moviesDto;
        }

        public async Task<IEnumerable<MoviesDto>> GetAllMoviesAsync(CancellationToken cancellationToken)
        {
            var movies = await this.context.Movies
                .OrderByDescending(e => e.RegisteredInDataBase)
                .ToListAsync(cancellationToken);

            var moviesDto = this.mapper
                .Map<List<MoviesDto>>(movies.Take(13));

            return moviesDto;
        }

        public async Task RemoveReviewedMovie(int id , CancellationToken cancellationToken)
        {
            var myMovies = await this.context.MyMovies
                 .Where(reviewedMovieId => reviewedMovieId.Id == id)
                .FirstAsync();

            var movie = await this.context.Movies
                .Where(title => title.Title == myMovies.Title)
                .FirstAsync();

            myMovies.IsWatched = true;
            movie.IsWatched = true;

            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> RateMovieAsync(RatingDto dto , CancellationToken cancellationToken)
        {
            if (dto.MovieId == 0)
            {
                return false;
            }

            var myMovie = await this.context.MyMovies
                .Where(id => id.Id == dto.MovieId)
                .SingleAsync();

            if (myMovie == null)
            {
                return false;
            }

            myMovie.Rate = dto.Rating;
            myMovie.IsRate = true;

            await this.context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<MyMoviesDto>> GetAllMyWatchedMoviesAsync(CancellationToken cancellationToken)
        {
            var movies = await this.context.MyMovies
                 .Where(reviewed => reviewed.IsRate == true && reviewed.IsWatched)
                .ToListAsync(cancellationToken);

            var moviesDto = this.mapper.Map<List<MyMoviesDto>>(movies);

            return moviesDto;
        }

    }
}
