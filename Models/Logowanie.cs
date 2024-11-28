using System;
using System.ComponentModel.DataAnnotations;
using WebApplication71.Models.Enums;

namespace WebApplication71.Models
{
    public class Logowanie
    {
        [Key]
        public string LogowanieId { get; private set; }
        public string DataLogowania { get; private set; }
        public string DataWylogowania { get; set; }
        public string CzasPracy { get; set; }
        public StatusZalogowania Status { get; set; }



        public string? UserId { get; private set; }
        public ApplicationUser? User { get; private set; }




        public Logowanie(string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = DateTime.Now.ToString();
            DataWylogowania = "01.01.0001 00:00:00";
            CzasPracy = "";
            Status = StatusZalogowania.Zalogowany;
            UserId = userId;
        }


        public Logowanie(string dataLogowania, string dataWylogowania, StatusZalogowania statusZalogowania, string userId)
        {
            LogowanieId = Guid.NewGuid().ToString();
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;

            TimeSpan cp = DateTime.Parse(DataLogowania) - DateTime.Parse(dataWylogowania);
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy.Duration().ToString();

            Status = statusZalogowania;
            UserId = userId;
        }



        public void Update(string dataLogowania, string dataWylogowania, StatusZalogowania statusZalogowania, string userId)
        {
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;

            TimeSpan cp = DateTime.Parse(DataLogowania) - DateTime.Parse(dataWylogowania);
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy.Duration().ToString();

            Status = statusZalogowania;
            UserId = userId;
        }


        /*public void DodajDateWylogowania(string dataWylogowania)
        {
            DataWylogowania = dataWylogowania;

            // obliczenie czasu pracy

            TimeSpan cp = DateTime.Parse(DataLogowania) - DateTime.Parse(dataWylogowania);
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy.Duration().ToString();

            Status = StatusZalogowania.Niezalogowany;
        }*/

        public void DodajDateWylogowania()
        {
            DataWylogowania = DateTime.Now.ToString ();

            // obliczenie czasu pracy

            TimeSpan cp = DateTime.Parse(DataLogowania) - DateTime.Parse(DataWylogowania);
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy.Duration().ToString();

            Status = StatusZalogowania.Niezalogowany;
        }

    }
}
