using AutoMapper;
using MoviesApi.Models;
using MoviesApiData;
using MoviesApiService.Model;

namespace MoviesApi.AutoMapperProfiles
{
    public class MyMoviesProfile : Profile
    {
        public MyMoviesProfile()
        {
            CreateMap<MyMovies, MyMoviesDto>().ReverseMap();
            CreateMap<MyMoviesViewModel, MyMoviesDto>().ReverseMap();
        }
    }
}
