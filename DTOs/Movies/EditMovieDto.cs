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
        public string Photo { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public double Price { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        public string CategoryId { get; set; }
    }
}
