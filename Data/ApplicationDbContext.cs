using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebApplication71.Models;
using WebApplication71.Models.Enums;

namespace WebApplication71.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private DataAutogenerator.NetCore.DataAutogenerator _dataAutogenerator = new DataAutogenerator.NetCore.DataAutogenerator();
        private Random rand = new Random();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Logowanie> Logowania { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(h => h.Movies).WithOne(w => w.User).HasForeignKey(h => h.UserId).OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(h => h.Logowania).WithOne(w => w.User).HasForeignKey(h => h.UserId).OnDelete(DeleteBehavior.ClientSetNull);


            builder.Entity<Category>()
                .HasMany(h => h.Movies).WithOne(w => w.Category).HasForeignKey(h => h.CategoryId).OnDelete(DeleteBehavior.ClientSetNull);



            // ROLES 

            var adminRole = new ApplicationRole("Administrator");
            var userRole = new ApplicationRole("User");
            builder.Entity<ApplicationRole>().HasData(adminRole, userRole);



            // USERS   

            string dataUrodzenia = new DateTime(rand.Next(1980, 2000), rand.Next(1, 12), rand.Next(1, 30), rand.Next(1, 24), rand.Next(1, 60), rand.Next(1, 60)).ToString();
            string pesel = rand.Next(100000000, 999999999).ToString();

            var administratorUser = new ApplicationUser(
                email: "admin@admin.pl",
                imie: _dataAutogenerator.Imie(),
                nazwisko: _dataAutogenerator.Nazwisko(),
                ulica: _dataAutogenerator.Imie(),
                miejscowosc: _dataAutogenerator.Nazwisko(),
                wojewodztwo: "Mazowieckie",
                pesel: pesel,
                dataUrodzenia: dataUrodzenia,
                plec: Plec.Mężczyzna,
                telefon: $"{rand.Next(100, 999)} {rand.Next(100, 999)} {rand.Next(100, 999)}",
                photo: "",
                password: "SDG%$@5423sdgagSDert"
                );
            IdentityUserRole<string> identityUserRoleAdmin = new IdentityUserRole<string>()
            {
                UserId = administratorUser.Id,
                RoleId = adminRole.Id
            };

            var userUser = new ApplicationUser(
                email: "user@user.pl",
                imie: _dataAutogenerator.Imie(),
                nazwisko: _dataAutogenerator.Nazwisko(),
                ulica: _dataAutogenerator.Imie(),
                miejscowosc: _dataAutogenerator.Nazwisko(),
                wojewodztwo: "Mazowieckie",
                pesel: pesel,
                dataUrodzenia: dataUrodzenia,
                plec: Plec.Mężczyzna,
                telefon: $"{rand.Next(100, 999)} {rand.Next(100, 999)} {rand.Next(100, 999)}",
                photo: "",
                password: "SDG%$@5423sdgagSDert"
                );
            IdentityUserRole<string> identityUserRoleUser = new IdentityUserRole<string>()
            {
                UserId = userUser.Id,
                RoleId = userRole.Id
            };


            var aaaUser = new ApplicationUser(
                email: "aaa@aaa.pl",
                imie: _dataAutogenerator.Imie(),
                nazwisko: _dataAutogenerator.Nazwisko(),
                ulica: _dataAutogenerator.Imie(),
                miejscowosc: _dataAutogenerator.Nazwisko(),
                wojewodztwo: "Mazowieckie",
                pesel: pesel,
                dataUrodzenia: dataUrodzenia,
                plec: Plec.Mężczyzna,
                telefon: $"{rand.Next(100, 999)} {rand.Next(100, 999)} {rand.Next(100, 999)}",
                photo: "",
                password: "SDG%$@5423sdgagSDert"
                );
            IdentityUserRole<string> identityUserRoleUserAaa = new IdentityUserRole<string>()
            {
                UserId = aaaUser.Id,
                RoleId = userRole.Id
            };



            var bbbUser = new ApplicationUser(
                email: "bbb@bbb.pl",
                imie: _dataAutogenerator.Imie(),
                nazwisko: _dataAutogenerator.Nazwisko(),
                ulica: _dataAutogenerator.Imie(),
                miejscowosc: _dataAutogenerator.Nazwisko(),
                wojewodztwo: "Mazowieckie",
                pesel: pesel,
                dataUrodzenia: dataUrodzenia,
                plec: Plec.Mężczyzna,
                telefon: $"{rand.Next(100, 999)} {rand.Next(100, 999)} {rand.Next(100, 999)}",
                photo: "",
                password: "SDG%$@5423sdgagSDert"
                );
            IdentityUserRole<string> identityUserRoleUserBbb = new IdentityUserRole<string>()
            {
                UserId = bbbUser.Id,
                RoleId = userRole.Id
            };


            var cccUser = new ApplicationUser(
                email: "ccc@ccc.pl",
                imie: _dataAutogenerator.Imie(),
                nazwisko: _dataAutogenerator.Nazwisko(),
                ulica: _dataAutogenerator.Imie(),
                miejscowosc: _dataAutogenerator.Nazwisko(),
                wojewodztwo: "Mazowieckie",
                pesel: pesel,
                dataUrodzenia: dataUrodzenia,
                plec: Plec.Mężczyzna,
                telefon: $"{rand.Next(100, 999)} {rand.Next(100, 999)} {rand.Next(100, 999)}",
                photo: "",
                password: "SDG%$@5423sdgagSDert"
                );
            IdentityUserRole<string> identityUserRoleUserCcc = new IdentityUserRole<string>()
            {
                UserId = cccUser.Id,
                RoleId = userRole.Id
            };


            var dddUser = new ApplicationUser(
                email: "ddd@ddd.pl",
                imie: _dataAutogenerator.Imie(),
                nazwisko: _dataAutogenerator.Nazwisko(),
                ulica: _dataAutogenerator.Imie(),
                miejscowosc: _dataAutogenerator.Nazwisko(),
                wojewodztwo: "Mazowieckie",
                pesel: pesel,
                dataUrodzenia: dataUrodzenia,
                plec: Plec.Mężczyzna,
                telefon: $"{rand.Next(100, 999)} {rand.Next(100, 999)} {rand.Next(100, 999)}",
                photo: "",
                password: "SDG%$@5423sdgagSDert"
                );
            IdentityUserRole<string> identityUserRoleUserDdd = new IdentityUserRole<string>()
            {
                UserId = dddUser.Id,
                RoleId = userRole.Id
            };


            builder.Entity<ApplicationUser>().HasData(administratorUser, userUser, aaaUser, bbbUser, cccUser, dddUser);
            builder.Entity<IdentityUserRole<string>>().HasData(identityUserRoleAdmin, identityUserRoleUser, identityUserRoleUserAaa, identityUserRoleUserBbb, identityUserRoleUserCcc, identityUserRoleUserDdd);





            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                aaaUser, bbbUser, cccUser, dddUser, userUser, administratorUser
            };


            List<string> kategorie = new List<string>() { "Komedia", "Romans", "Fantasy" };
            List<string> kategorieId = new List<string>();
            for (var i = 0; i < kategorie.Count; i++)
            {
                Category category = new Category(kategorie[i]);
                builder.Entity<Category>().HasData(category);
                kategorieId.Add(category.CategoryId);
            }



            List<string> photoSource = new List<string>()
            {
                "https://th.bing.com/th/id/OIP.ezcZDNwC18nndTJ-mWSmWAHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.ijnbIXDEFkej1FbYkIiW3gHaKM?w=186&h=257&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.pvYWuPXi0Jpw9469VHY1lgHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.ykllnoTUaAyNHNmm1cIXqgHaKE?w=186&h=253&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.Ybzxe9qfOcMv4wbzxLttnQHaLb?w=193&h=298&c=7&r=0&o=5&dpr=1.6&pid=1.7",
                "https://th.bing.com/th/id/OIP.muhnh4C5aC98tY8rGeijmQHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.z-BvaMHwT-e4eDGIU910HwHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.Ruer00AChRpCRJkyHXki4gHaK-?w=186&h=276&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.uFoNnnrV5pCY5ZHxErsQCgHaKa?w=186&h=262&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.Gx8PamDxLVy90q2suVI7_wHaKg?w=186&h=264&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.vkzBGI8duWq91j57EAEDpQHaKm?w=186&h=267&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.MvEB2xCFCj3-Ibi6Z2r8GAHaKj?w=186&h=265&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.ezcZDNwC18nndTJ-mWSmWAHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.ijnbIXDEFkej1FbYkIiW3gHaKM?w=186&h=257&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.pvYWuPXi0Jpw9469VHY1lgHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.ykllnoTUaAyNHNmm1cIXqgHaKE?w=186&h=253&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.iCmMgQrEpmif7msCLbZVhgHaLT?w=186&h=284&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.muhnh4C5aC98tY8rGeijmQHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.z-BvaMHwT-e4eDGIU910HwHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.Ruer00AChRpCRJkyHXki4gHaK-?w=186&h=276&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.uFoNnnrV5pCY5ZHxErsQCgHaKa?w=186&h=262&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.Gx8PamDxLVy90q2suVI7_wHaKg?w=186&h=264&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.vkzBGI8duWq91j57EAEDpQHaKm?w=186&h=267&c=7&r=0&o=5&dpr=1.8&pid=1.7",
                "https://th.bing.com/th/id/OIP.MvEB2xCFCj3-Ibi6Z2r8GAHaKj?w=186&h=265&c=7&r=0&o=5&dpr=1.8&pid=1.7"
            };
            for (var i = 0; i < 24; i++)
            {
                var randomUser = users[rand.Next(0, users.Count - 1)];
                Movie movie = new Movie(
                    _dataAutogenerator.Title(),
                    _dataAutogenerator.Description(1),
                    photoSource[rand.Next(0, photoSource.Count)],
                    rand.Next(100, 200),
                    administratorUser.Id,
                    kategorieId[rand.Next(0, kategorieId.Count)]
                    );
                builder.Entity<Movie>().HasData(movie);
            }



            for (var j = 0; j < rand.Next(1, 10); j++)
            {
                var dataZalogowania = DateTime.Now.AddDays(-rand.Next(10, 100));
                var dataWylogowania = dataZalogowania.AddMinutes(rand.Next(150, 480));
                string userId = users[rand.Next(0, users.Count)].Id;
                Logowanie logowanie = new Logowanie(
                    userId: userId
                    );
                builder.Entity<Logowanie>().HasData(logowanie);
            }




            base.OnModelCreating(builder);
        }



    }
}
