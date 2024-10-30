using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models;

namespace WebApplication71.DTOs.Categories
{
    public class GetCategoryDto
    {
        public string CategoryId { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Name { get; set; }




        public List<Movie> Movies { get; set; }
    }
}
