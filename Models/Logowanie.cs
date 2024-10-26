using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Models
{
    public class Logowanie
    {
        [Key]
        public string LogowanieId { get; private set; }

        [Required, DataType(DataType.DateTime)]
        public string DataLogowania { get; private set; }

        [Required, DataType(DataType.DateTime)]
        public string DataWylogowania { get; private set; }
        public string CzasPracy { get; private set; }



        public string? UserId { get; private set; }
        public ApplicationUser? User { get; private set; }



        public Logowanie(string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = DateTime.Now.ToString();
            DataWylogowania = "";
            UserId = userId;
        }


        public Logowanie(string dataLogowania, string dataWylogowania, string czasPracy, string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;
            CzasPracy = czasPracy;
            UserId = userId;
        }



        public void Update(string dataLogowania, string dataWylogowania, string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;
            UserId = userId;



            // obliczenie czasu pracy

            var dz = DateTime.Parse(dataLogowania);
            var dw = DateTime.Parse(dataWylogowania);
            var czasPracy = dz - dw;
            CzasPracy = czasPracy.ToString();
        }

        public void DodajDateWylogowania(string dataWylogowania)
        {
            DataWylogowania = dataWylogowania;

            // obliczenie czasu pracy

            var dz = DateTime.Parse(DataLogowania);
            var dw = DateTime.Parse(dataWylogowania);
            var czasPracy = dw - dz;
            CzasPracy = czasPracy.ToString();
        }



    }
}
