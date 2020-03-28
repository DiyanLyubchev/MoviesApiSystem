using System;
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

        private readonly IWebService webService;

        private readonly IMoviesService service;

        private readonly IMapper mapper;
        public HomeController(ILogger<HomeController> logger, IMoviesService service, IMapper mapper, IWebService webService)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
            this.webService = webService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var allMovies = await this.service.GetAllMoviesAsync(cancellationToken);

            var listMovieViewModel = this.mapper.Map<List<МoviesViewModel>>(allMovies);

            return View(listMovieViewModel);
        }

        public async Task<IActionResult> GetNewMovies(CancellationToken cancellationToken)
        {
            await this.webService.GetNewMovies(cancellationToken);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
