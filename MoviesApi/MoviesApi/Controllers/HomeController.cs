﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
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

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var allMovies = await this.service.GetAllMoviesAsync(cancellationToken);

            var listMovieViewModel = this.mapper.Map<List<МoviesViewModel>>(allMovies);

            return View(listMovieViewModel);
        }

        public async Task<IActionResult> GetNewMovies(CancellationToken cancellationToken)
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
                        Year = item.movie.year,
                        RegisteredInDataBase = DateTime.Now
                    });
                }
                await this.service.AddMovieToDataAsync(listDto , cancellationToken);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
