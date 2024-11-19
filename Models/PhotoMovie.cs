using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Models
{
    public class PhotoMovie
    {
        [Key]
        public string PhotoMovieId { get; set; }
        public byte[] PhotoData { get; set; }


        public string MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
