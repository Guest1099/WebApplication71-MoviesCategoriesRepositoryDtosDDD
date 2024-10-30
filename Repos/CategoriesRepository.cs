using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication71.Data;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Categories;
using WebApplication71.Models;
using WebApplication71.Repos.Abs;

namespace WebApplication71.Repos
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ResultViewModel<List<GetCategoryDto>>> GetAll()
        {
            var resultViewModel = new ResultViewModel<List<GetCategoryDto>>() { Success = false, Message = "", Object = new List<GetCategoryDto>() };
            try
            {
                var categories = await _context.Categories
                    .Include(i => i.Movies)
                    .ToListAsync();
                if (categories != null)
                {
                    resultViewModel.Success = true;
                    resultViewModel.Object = categories.Select(
                        s => new GetCategoryDto()
                        {
                            CategoryId = s.CategoryId,
                            Name = s.Name,
                            Movies = s.Movies
                        })
                            .ToList();
                }
                else
                {
                    resultViewModel.Message = "Categories was null";
                }
            }
            catch (Exception ex)
            {
                resultViewModel.Message = $"Exception: {ex.Message}";
            }

            return resultViewModel;
        }




        public async Task<ResultViewModel<GetCategoryDto>> Get(string categoryId)
        {
            var resultViewModel = new ResultViewModel<GetCategoryDto>() { Success = false, Message = "", Object = new GetCategoryDto() };

            if (!string.IsNullOrEmpty(categoryId))
            {
                try
                {
                    var category = await _context.Categories
                            .Include(i => i.Movies)
                            .FirstOrDefaultAsync(f => f.CategoryId == categoryId);
                    if (category != null)
                    {
                        resultViewModel.Success = true;
                        resultViewModel.Object = new GetCategoryDto()
                        {
                            CategoryId = category.CategoryId,
                            Name = category.Name
                        };
                    }
                    else
                    {
                        resultViewModel.Message = "Category was null";
                    }
                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Id was null";
            }

            return resultViewModel;
        }




        public async Task<ResultViewModel<CreateCategoryDto>> Create(CreateCategoryDto model)
        {
            var resultViewModel = new ResultViewModel<CreateCategoryDto>() { Success = false, Message = "", Object = new CreateCategoryDto() };

            if (model != null)
            {
                try
                {
                    // sprawdza czy podana nazwa kategorii już istnieje, jeśli nie to dodaje rekord, jeśli tak to zwraca komunikat
                    if ((await _context.Categories.FirstOrDefaultAsync(f => f.Name == model.Name)) == null)
                    {
                        Category category = new Category(model.Name);

                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();

                        resultViewModel.Success = true;
                        resultViewModel.Object = model;
                    }
                    else
                    {
                        resultViewModel.Message = "Wskazana nazwa kategorii już istnieje";
                    }
                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Model was null";
            }
            return resultViewModel;
        }





        public async Task<ResultViewModel<EditCategoryDto>> Update(EditCategoryDto model)
        {
            var resultViewModel = new ResultViewModel<EditCategoryDto>() { Success = false, Message = "", Object = new EditCategoryDto() };

            if (model != null)
            {
                try
                {
                    // sprawdza czy podana nazwa kategorii już istnieje, jeśli nie to dodaje rekord, jeśli tak to zwraca komunikat
                    if ((await _context.Categories.FirstOrDefaultAsync(f => f.Name == model.Name && f.CategoryId != model.CategoryId)) == null)
                    {
                        var category = await _context.Categories.FirstOrDefaultAsync(f => f.CategoryId == model.CategoryId);
                        if (category != null)
                        {
                            category.UpdateCategory(
                                name: category.Name
                                );

                            _context.Entry(category).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            resultViewModel.Success = true;
                            resultViewModel.Object = model;
                        }
                        else
                        {
                            resultViewModel.Message = "Category was null";
                        }
                    }
                    else
                    {
                        resultViewModel.Message = "Wskazana nazwa kategorii już istnieje";
                    }


                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Model was null";
            }
            return resultViewModel;
        }




        public async Task<ResultViewModel<bool>> Delete(string categoryId)
        {
            var resultViewModel = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(categoryId))
            {
                try
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(f => f.CategoryId == categoryId);
                    if (category != null)
                    {
                        _context.Categories.Remove(category);
                        await _context.SaveChangesAsync();

                        resultViewModel.Success = true;
                        resultViewModel.Object = true;
                    }
                    else
                    {
                        resultViewModel.Message = "Category was null";
                    }
                }
                catch (Exception ex)
                {
                    resultViewModel.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                resultViewModel.Message = "Id was null";
            }
            return resultViewModel;
        }

    }
}
