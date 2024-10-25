using System.ComponentModel.DataAnnotations;

namespace WebApplication71.DTOs.Logowania
{
    public class GetLogowanieDto
    {
        public string LogowanieId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DataLogowania { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DataWylogowania { get; set; }
        public string CzasPracy { get; set; }



        public string Email { get; set; }
    }
}
