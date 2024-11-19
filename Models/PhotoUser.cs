using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Models
{
    public class PhotoUser
    {
        [Key]
        public string PhotoUserId { get; set; }
        public byte[] PhotoData { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
