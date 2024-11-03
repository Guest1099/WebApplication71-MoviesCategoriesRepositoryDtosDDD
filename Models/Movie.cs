using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Models
{
    public class Movie
    {

        [Key]
        public string MovieId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public byte [] Photo { get; private set; }
        public double Price { get; private set; }
        public int Klikniecia { get; private set; }
        public DateTime DataDodania { get; private set; }




        public string? UserId { get; private set; }
        public ApplicationUser? User { get; private set; }


        public string? CategoryId { get; private set; }
        public Category? Category { get; private set; }




        public Movie(string title, string description, byte[] photo, double price, string userId, string categoryId)
        {
            MovieId = Guid.NewGuid().ToString();
            Title = title;
            Description = description;
            Photo = photo;
            Price = price;
            UserId = userId;
            CategoryId = categoryId;
            Klikniecia = 0;
            DataDodania = DateTime.Now;
        }


        public void Update(string title, string description, byte[] photo, double price, string categoryId)
        {
            Title = title;
            Description = description;
            Photo = photo;
            Price = price;
            CategoryId = categoryId;
        }

    }
}
