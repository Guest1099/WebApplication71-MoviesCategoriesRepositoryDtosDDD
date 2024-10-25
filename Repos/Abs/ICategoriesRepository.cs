using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Categories;

namespace WebApplication71.Repos.Abs
{
    public interface ICategoriesRepository
    {
        Task<ResultViewModel<List<GetCategoryDto>>> GetAll();
        Task<ResultViewModel<GetCategoryDto>> Get(string categoryId);
        Task<ResultViewModel<CreateCategoryDto>> Create(CreateCategoryDto model);
        Task<ResultViewModel<EditCategoryDto>> Update(EditCategoryDto model);
        Task<ResultViewModel<bool>> Delete(string categoryId);
    }
}
