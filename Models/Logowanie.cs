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



        public void Update(string dataLogowania, string dataWylogowania, string userId)
        {
            DataLogowania = dataLogowania;
            DataWylogowania = dataWylogowania;

            TimeSpan cp = DateTime.Parse(DataLogowania) - DateTime.Parse(dataWylogowania);
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy.Duration().ToString();

            UserId = userId;
        }


        public void DodajDateWylogowania(string dataWylogowania)
        {
            DataWylogowania = dataWylogowania;

            // obliczenie czasu pracy

            TimeSpan cp = DateTime.Parse(DataLogowania) - DateTime.Parse(dataWylogowania);
            TimeSpan czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
            CzasPracy = czasPracy.Duration().ToString();

            Status = StatusZalogowania.Niezalogowany;
        }








        /*
                public Logowanie(string userId)
                {
                    LogowanieId = Guid.NewGuid().ToString();
                    DataLogowania = DateTime.Now.ToString();
                    DataWylogowania = "DateTime.MinValue";
                    UserId = userId;
                }


                public Logowanie(DateTime dataLogowania, DateTime dataWylogowania, string userId)
                {
                    *//*LogowanieId = Guid.NewGuid().ToString();
                    DataLogowania = new DateTime(dataLogowania.Year, dataLogowania.Month, dataLogowania.Day, dataLogowania.Hour, dataLogowania.Minute, dataLogowania.Second);
                    DataWylogowania = new DateTime(dataWylogowania.Year, dataWylogowania.Month, dataWylogowania.Day, dataWylogowania.Hour, dataWylogowania.Minute, dataWylogowania.Second);
                    var cp = dataWylogowania - dataLogowania;
                    CzasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
                    UserId = userId;*//*

                    LogowanieId = Guid.NewGuid().ToString();
                    DataLogowania = dataLogowania;
                    DataWylogowania = dataWylogowania; 
                    var cp = dataWylogowania - dataLogowania;
                    //var czasPracy = new TimeSpan(cp.Days, cp.Hours, cp.Minutes, cp.Seconds);
                    var czasPracy = new TimeSpan(11,1,1,1,1);
                    CzasPracy = czasPracy.ToString ();
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
        */


    }
}
