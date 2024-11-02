using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Logowania;

namespace WebApplication71.Repos.Abs
{
    public interface ILogowaniaRepository
    {
        Task<ResultViewModel<List<GetLogowanieDto>>> GetAll(string email);
        Task<ResultViewModel<GetLogowanieDto>> Get(string logowanieId);
        Task<ResultViewModel<CreateLogowanieDto>> Create(CreateLogowanieDto model);
        Task<ResultViewModel<EditLogowanieDto>> Update(EditLogowanieDto model);
        Task<ResultViewModel<bool>> Delete(string logowanieId);
    }
}
