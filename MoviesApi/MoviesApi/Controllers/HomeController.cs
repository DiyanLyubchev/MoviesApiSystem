using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApi.Models;
using MoviesApiService;
using Newtonsoft.Json;

namespace MoviesApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        private readonly IMoviesService service;

        private readonly IMapper mapper;
        public HomeController(ILogger<HomeController> logger, IMoviesService service, IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var allMovies = await this.service.GetAllMoviesAsync();

            var listMovieViewModel = this.mapper.Map<List<FilmМoviesViewModel>>(allMovies);

            return View(listMovieViewModel);
        }

        public async Task<IActionResult> GetNewMovies()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Add("trakt-api-version", "2");
                httpClient.DefaultRequestHeaders.Add("trakt-api-key", "402fba05ed79a82efcf250432e3796198b6a23bcd62d16abd0640d114aaeaa8d");
                var trending = await httpClient.GetStringAsync("https://api.trakt.tv/movies/trending");

                var listDto = new List<MoviesDto>();
                var trendingJSON = JsonConvert.DeserializeObject<dynamic>(trending);
                foreach (var item in trendingJSON)
                {
                    listDto.Add(new MoviesDto
                    {
                        Title = item.movie.title,
                        IMDB = item.movie.ids.imdb,
                        Year = item.movie.year
                    });
                }
                await this.service.AddMovieToDataAsync(listDto);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddMyFavoriteMovies(int moviesId)
        {
            var movieDto = await this.service.FindByIdAsync(moviesId);

            await this.service.AddToMyFavoriteAsync(movieDto);

            return Json(new { movie = moviesId });
        }

        public async Task<IActionResult> ShowMyMovie()
        {
            var myMovie = await this.service.GetMyMoviesAsync();
            var listMovieViewModel = this.mapper.Map<List<MyMoviesViewModel>>(myMovie);

            return View(listMovieViewModel);
        }
          

        public async Task<IActionResult> ReviewedMovie(int reviewedMovieId)
        {
            await this.service.RemoveReviewedMovie(reviewedMovieId);

            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
