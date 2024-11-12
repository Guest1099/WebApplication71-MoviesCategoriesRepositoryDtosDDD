using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Models
{
    public class Logowanie
    {
        [Key]
        public string LogowanieId { get; private set; }
        public DateTime DataLogowania { get; private set; }
        public DateTime DataWylogowania { get; set; }
        public TimeSpan CzasPracy { get; set; }



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
            DataLogowania = new DateTime(dataLogowania.Year, dataLogowania.Month, dataLogowania.Day, dataLogowania.Hour, dataLogowania.Minute, dataLogowania.Second);
            DataWylogowania = new DateTime(dataWylogowania.Year, dataWylogowania.Month, dataWylogowania.Day, dataWylogowania.Hour, dataWylogowania.Minute, dataWylogowania.Second);
            var cp = dataWylogowania - dataLogowania;
            CzasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            UserId = userId;
        }



        public Logowanie(DateTime dataLogowania, DateTime dataWylogowania, TimeSpan czasPracy, string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = new DateTime(dataLogowania.Year, dataLogowania.Month, dataLogowania.Day, dataLogowania.Hour, dataLogowania.Minute, dataLogowania.Second);
            DataWylogowania = new DateTime(dataWylogowania.Year, dataWylogowania.Month, dataWylogowania.Day, dataWylogowania.Hour, dataWylogowania.Minute, dataWylogowania.Second);
            CzasPracy = czasPracy;
            UserId = userId;
        }


        public void Update(DateTime dataLogowania, DateTime dataWylogowania, string userId)
        {
            DataLogowania = new DateTime(dataLogowania.Year, dataLogowania.Month, dataLogowania.Day, dataLogowania.Hour, dataLogowania.Minute, dataLogowania.Second);
            DataWylogowania = new DateTime(dataWylogowania.Year, dataWylogowania.Month, dataWylogowania.Day, dataWylogowania.Hour, dataWylogowania.Minute, dataWylogowania.Second);
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
