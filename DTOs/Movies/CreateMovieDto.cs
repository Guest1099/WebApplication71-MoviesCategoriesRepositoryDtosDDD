using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Movies
{
    public class CreateMovieDto
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Title { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Description { get; set; }


        public byte [] Photo { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Wprowadź poprawną liczbę")]
        public double Price { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string CategoryId { get; set; }




        public IFormFile PhotoData { get; set; }
        public string Email { get; set; }
        public SelectList CategoriesList { get; set; }
    }
}
