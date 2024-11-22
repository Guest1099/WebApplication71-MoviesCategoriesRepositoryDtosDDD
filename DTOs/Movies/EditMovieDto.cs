﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Movies
{
    public class EditMovieDto
    {
        public string MovieId { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Title { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Description { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Wprowadź poprawną liczbę")]
        public double Price { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string CategoryId { get; set; }



        public List <IFormFile> Files { get; set; }
    }
}
