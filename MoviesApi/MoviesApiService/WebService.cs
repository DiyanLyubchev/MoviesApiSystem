using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApiService
{
    public class WebService : IWebService
    {
        private readonly IMoviesService service;


        public WebService(IMoviesService service)
        {
            this.service = service;
        }

        public async Task GetNewMovies(CancellationToken cancellationToken)
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
                var trendingJSON = JsonConvert.DeserializeObject<MoviesDto[]>(trending);
                foreach (var item in trendingJSON)
                {
                    listDto.Add(item);
                }
                await this.service.AddMovieToDataAsync(listDto, cancellationToken);
            }
        }
    }
}
