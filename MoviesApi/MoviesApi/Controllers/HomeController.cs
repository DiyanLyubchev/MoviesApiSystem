using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApi.Models;
using MoviesApiService;

namespace MoviesApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        private readonly IWebService webService;

        private readonly IMoviesService service;

        private readonly IMapper mapper;

        private const int PageSize = 5;
        public HomeController(ILogger<HomeController> logger, IMoviesService service, IMapper mapper, IWebService webService)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
            this.webService = webService;
        }

        public async Task<IActionResult> Index(int id,CancellationToken cancellationToken)
        {
            var allMovies = await this.service.GetAllMoviesAsync(cancellationToken);
            var count = allMovies.Count();
            var data = allMovies.OrderBy(x => x.Id).Skip(id * PageSize).Take(PageSize).ToList();

            var listMovieViewModel = this.mapper.Map<List<МoviesViewModel>>(data);
            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            this.ViewBag.Page = id;
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
