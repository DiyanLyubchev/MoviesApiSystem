using AutoMapper;
using MoviesApi.Models;
using MoviesApiData;

namespace MoviesApiService.AutoMapperProfiles
{
    public class MoviesProfile : Profile
    {

        public MoviesProfile()
        {
            CreateMap<Movies, MoviesDto>().ReverseMap();
            CreateMap<MoviesDto, МoviesViewModel>().ReverseMap();
        }
    }
}
