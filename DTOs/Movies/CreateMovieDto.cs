using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Movies
{
    public class CreateMovieDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string CategoryId { get; set; }



        public string Email { get; set; }
        public SelectList CategoriesList { get; set; }
    }
}
