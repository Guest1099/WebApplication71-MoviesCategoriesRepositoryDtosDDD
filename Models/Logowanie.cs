using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Models
{
    public class Logowanie
    {
        [Key]
        public string LogowanieId { get; private set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DataLogowania { get; private set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DataWylogowania { get; private set; }
        public TimeSpan CzasPracy { get; private set; }



        public string? UserId { get; private set; }
        public ApplicationUser? User { get; private set; }



        public Logowanie(string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = DateTime.Now;
            DataWylogowania = DateTime.MinValue;
            UserId = userId;
        }


        public Logowanie(DateTime dataLogowania, DateTime dataWylogowania, string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;
            var cp = dataWylogowania - dataLogowania;
            CzasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            UserId = userId;
        }



        public Logowanie(DateTime dataLogowania, DateTime dataWylogowania, TimeSpan czasPracy, string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;
            CzasPracy = czasPracy;
            UserId = userId;
        }


        public void Update(DateTime dataLogowania, DateTime dataWylogowania, string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;
            UserId = userId;


            var cp = dataWylogowania - dataLogowania;
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy;
        }

        public void DodajDateWylogowania(DateTime dataWylogowania)
        {
            DataWylogowania = dataWylogowania;

            // obliczenie czasu pracy

            var dz = DataLogowania;
            var dw = dataWylogowania;
            var cp = dw - dz;
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy;
        }



    }
}
