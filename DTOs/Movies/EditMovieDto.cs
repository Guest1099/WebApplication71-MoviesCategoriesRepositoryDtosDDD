using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Movies
{
    public class EditMovieDto
    {
        public string MovieId { get; set; }

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
    }
}
