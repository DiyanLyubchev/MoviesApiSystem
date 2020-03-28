using System.Threading;
using System.Threading.Tasks;

namespace MoviesApiService
{
    public interface IWebService
    {
        Task GetNewMovies(CancellationToken cancellationToken);
    }
}