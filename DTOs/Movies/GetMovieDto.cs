using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models;

namespace WebApplication71.DTOs.Movies
{
    public class GetMovieDto
    {
        public string MovieId { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Title { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Description { get; set; }


        public byte[] Photo { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Wprowadź poprawną liczbę")]
        public double Price { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string CategoryId { get; set; }




        public IFormFile PhotoData { get; set; }
        public string Category { get; set; }
        public SelectList CategoriesList { get; set; }

        
        public List <PhotoUser> PhotosUser { get; set; }
    }
}
