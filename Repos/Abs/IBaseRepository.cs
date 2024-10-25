using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication71.Repos.Abs
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(string id);
        Task Create(T model);
        Task Delete(string id);
        Task Update(T model);
    }
}
