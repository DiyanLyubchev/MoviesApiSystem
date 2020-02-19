using MoviesApiService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApiService
{
    public interface IMoviesService
    {
        Task AddToMyFavoriteAsync(MoviesDto dto);

        Task<IEnumerable<MoviesDto>> GetAllMoviesAsync();

        Task AddMovieToDataAsync(List<MoviesDto> dto);

        Task<MoviesDto> FindByIdAsync(int id);

        Task<IEnumerable<MyMoviesDto>> GetMyMoviesAsync();

        Task RemoveReviewedMovie(int id);
    }
}