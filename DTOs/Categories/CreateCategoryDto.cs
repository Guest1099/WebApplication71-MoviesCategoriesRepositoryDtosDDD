using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Categories
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }

    }
}
