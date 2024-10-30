using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication71.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; private set; }
        public string Name { get; private set; }



        public List<Movie>? Movies { get; private set; }




        public Category(string name)
        {
            CategoryId = Guid.NewGuid().ToString();
            Name = name;

            Movies = new List<Movie>();
        }


        public void AddMovie(Movie movie)
        {
            if (!Movies.Contains(movie))
                Movies.Add(movie);
        }

        public void RemoveMovie(Movie movie)
        {
            Movies.Remove(movie);
        }

        public void UpdateCategory(string name)
        {
            Name = name;
        }


    }
}
