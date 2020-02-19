using MoviesApiData;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;

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

        public async Task AddMovieToDataAsync(List<MoviesDto> dto)
        {
            var listMovies = mapper.Map<List<Movies>>(dto);

            

            await this.context.Movies.AddRangeAsync(listMovies);
            await this.context.SaveChangesAsync();
        }

        public async Task AddToMyFavoriteAsync(MoviesDto dto)
        {
            var myMovie = mapper.Map<Movies>(dto);

            await this.context.Movies.AddAsync(myMovie);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MoviesDto>> GetAllMoviesAsync()
        {
            var movies = await this.context.Movies
                .ToListAsync();

            var moviesDto = mapper.Map<List<MoviesDto>>(movies);

            return moviesDto;
        }
    }
}
