﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApiService;
using MoviesApiService.Model;

namespace MoviesApi.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMoviesService service;

        private readonly IMapper mapper;
        public MovieController(IMoviesService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IActionResult> AddMyFavoriteMovies(int moviesId , CancellationToken cancellationToken)
        {
            var movieDto = await this.service.FindByIdAsync(moviesId);

            await this.service.AddToMyFavoriteAsync(movieDto , cancellationToken);

            return Json(new { movie = moviesId });
        }

        public async Task<IActionResult> ShowMyMovie(CancellationToken cancellationToken)
        {
            var myMovie = await this.service.GetMyMoviesAsync(cancellationToken);
            var listMovieViewModel = this.mapper.Map<List<MyMoviesViewModel>>(myMovie);

            return View(listMovieViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReviewedMovie(int reviewedMovieId , CancellationToken cancellationToken)
        {
            await this.service.RemoveReviewedMovie(reviewedMovieId , cancellationToken);

            return Json(new { reviewedId = reviewedMovieId });

        }

        public async Task<IActionResult> RateMovie([FromQuery]string data , CancellationToken cancellationToken)
        {
            var ratingMovie = data.Split(' ');

            double rating = 0;
            var movieId = 0;

            if (ratingMovie.Length == 3)
            {
                rating = double.Parse(ratingMovie[0]);
                movieId = int.Parse(ratingMovie[2]);
            }
            else if (ratingMovie.Length == 2)
            {
                rating = double.Parse(ratingMovie[0]);
                movieId = int.Parse(ratingMovie[1]);
            }


            var dto = new RatingDto
            {
                Rating = rating,
                MovieId = movieId,
            };

            await this.service.RateMovieAsync(dto , cancellationToken);

            return View();
        }

        public async Task<IActionResult> GetMyAllRateAndReviewedMovies(CancellationToken cancellationToken)
        {
            var listMovies = await this.service.GetAllMyWatchedMoviesAsync(cancellationToken);

            var viewList = this.mapper.Map<List<MyMoviesViewModel>>(listMovies);

            return View(viewList);

        }
    }
}