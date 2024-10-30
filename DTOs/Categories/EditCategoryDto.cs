using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Categories
{
    public class EditCategoryDto
    {
        public string CategoryId { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }
    }
}
