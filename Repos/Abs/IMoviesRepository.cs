using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Movies;

namespace WebApplication71.Repos.Abs
{
    public interface IMoviesRepository
    {
        Task<ResultViewModel<List<GetMovieDto>>> GetAll();
        Task<ResultViewModel<GetMovieDto>> Get(string movieId);
        Task<ResultViewModel<CreateMovieDto>> Create(CreateMovieDto model);
        Task<ResultViewModel<EditMovieDto>> Update(EditMovieDto model);
        Task<ResultViewModel<bool>> Delete(string movieId);
    }
}
