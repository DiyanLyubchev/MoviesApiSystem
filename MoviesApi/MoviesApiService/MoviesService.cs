﻿using MoviesApiData;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using MoviesApiService.Model;

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

            await this.context.SaveChangesAsync();
        }

        public async Task<MoviesDto> FindByIdAsync(int id)
        {
            var movie = await this.context.Movies
                .FindAsync(id);

            return this.mapper.Map<MoviesDto>(movie);
        }

        public async Task AddToMyFavoriteAsync(MoviesDto dto)
        {

            var myMovie = new MyMovies
            {
                Title = dto.Title,
                Year = dto.Year,
                IMDB = dto.IMDB,
                AddOn = DateTime.Now
            };

            await this.context.MyMovies.AddAsync(myMovie);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyMoviesDto>> GetMyMoviesAsync()
        {
            var movies = await this.context.MyMovies
                 .Where(reviewed => reviewed.IsWatched == false)
                .ToListAsync();

            var moviesDto = this.mapper.Map<List<MyMoviesDto>>(movies);

            return moviesDto;
        }

        public async Task<IEnumerable<MoviesDto>> GetAllMoviesAsync()
        {
            var movies = await this.context.Movies
                .OrderByDescending(e => e.RegisteredInDataBase)
                .ToListAsync();

            var moviesDto = this.mapper
                .Map<List<MoviesDto>>(movies.Take(10));

            return moviesDto;
        }

        public async Task RemoveReviewedMovie(int id)
        {
            var myMovies = await this.context.MyMovies
                 .Where(reviewedMovieId => reviewedMovieId.Id == id)
                .FirstAsync();

            var movie = await this.context.Movies
                .Where(title => title.Title == myMovies.Title)
                .FirstAsync();

            myMovies.IsWatched = true;
            movie.IsWatched = true;

            await this.context.SaveChangesAsync();
        }

        public async Task<bool> RateMovieAsync(RatingDto dto)
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

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<MyMoviesDto>> GetAllMyWatchedMoviesAsync()
        {
            var movies = await this.context.MyMovies
                 .Where(reviewed => reviewed.IsRate == true && reviewed.IsWatched)
                .ToListAsync();

            var moviesDto = this.mapper.Map<List<MyMoviesDto>>(movies);

            return moviesDto;
        }

    }
}
