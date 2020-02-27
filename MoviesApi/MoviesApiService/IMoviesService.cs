using MoviesApiService.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApiService
{
    public interface IMoviesService
    {
        Task AddToMyFavoriteAsync(MoviesDto dto , CancellationToken cancellationToken);

        Task<IEnumerable<MoviesDto>> GetAllMoviesAsync(CancellationToken cancellationToken);

        Task AddMovieToDataAsync(List<MoviesDto> dto , CancellationToken cancellationToken);

        Task<MoviesDto> FindByIdAsync(int id );

        Task<IEnumerable<MyMoviesDto>> GetMyMoviesAsync(CancellationToken cancellationToken);

        Task RemoveReviewedMovie(int id , CancellationToken cancellationToken);

        Task<bool> RateMovieAsync(RatingDto dto , CancellationToken cancellationToken);

        Task<IEnumerable<MyMoviesDto>> GetAllMyWatchedMoviesAsync(CancellationToken cancellationToken);
    }
}