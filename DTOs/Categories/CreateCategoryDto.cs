using System.ComponentModel.DataAnnotations;
using WebApplication71.Services;

namespace WebApplication71.DTOs.Categories
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }

    }
}
