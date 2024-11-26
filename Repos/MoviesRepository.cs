using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WebApplication71.Data;
using WebApplication71.DTOs;
using WebApplication71.DTOs.Movies;
using WebApplication71.Models;
using WebApplication71.Repos.Abs;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication71.Repos
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly HtmlSanitizer _htmlSanitizer;

        public MoviesRepository(ApplicationDbContext context, HtmlSanitizer htmlSanitizer)
        {
            _context = context;
            _htmlSanitizer = htmlSanitizer;
        }


        public async Task<ResultViewModel<List<GetMovieDto>>> GetAll()
        {
            var returnResult = new ResultViewModel<List<GetMovieDto>>() { Success = false, Message = "", Object = new List<GetMovieDto>() };
            try
            {
                var movies = await _context.Movies
                    .Include(i => i.Category)
                    .Include (i=> i.PhotosMovie)
                    .OrderByDescending(o => o.DataDodania)
                    .ToListAsync();

                if (movies != null)
                {
                    returnResult.Success = true;
                    returnResult.Object = movies.Select(
                        s => new GetMovieDto()
                        {
                            MovieId = s.MovieId,
                            Title = s.Title,
                            Description = s.Description,
                            Price = s.Price,
                            CategoryId = s.CategoryId,
                            Category = s.Category.Name,
                            PhotosMovie = s.PhotosMovie
                        })
                            .ToList();
                }
                else
                {
                    returnResult.Message = "Movies was null";
                }
            }
            catch (Exception ex)
            {
                returnResult.Message = $"Exception: {ex.Message}";
            }

            return returnResult;
        }




        public async Task<ResultViewModel<GetMovieDto>> Get(string movieId)
        {
            var returnResult = new ResultViewModel<GetMovieDto>() { Success = false, Message = "", Object = new GetMovieDto() };

            if (!string.IsNullOrEmpty(movieId))
            {
                try
                {
                    var movie = await _context.Movies
                        .Include(i => i.Category)
                        .Include (i=> i.PhotosMovie)
                        .FirstOrDefaultAsync(f => f.MovieId == movieId);

                    if (movie != null)
                    {
                        returnResult.Success = true;
                        returnResult.Object = new GetMovieDto()
                        {
                            Title = movie.Title,
                            Description = movie.Description,
                            Price = movie.Price,
                            CategoryId = movie.CategoryId,
                            Category = movie.Category.Name,
                            PhotosMovie = movie.PhotosMovie
                        };
                    }
                    else
                    {
                        returnResult.Message = "Movie was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Id was null";
            }


            return returnResult;
        }





        public async Task<ResultViewModel<CreateMovieDto>> Create(CreateMovieDto model)
        {
            var returnResult = new ResultViewModel<CreateMovieDto>() { Success = false, Message = "", Object = new CreateMovieDto() };

            if (model != null && !string.IsNullOrEmpty(model.Email))
            {
                try
                {
                    // znajdź zalogowanego użytkownika, po to aby dodać jego id do filmu 
                    var zalogowanyUser = await _context.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                    if (zalogowanyUser != null)
                    {
                        string movieId = Guid.NewGuid().ToString();

                        string title = _htmlSanitizer.Sanitize(model.Title);
                        title = HttpUtility.HtmlDecode(title);

                        string description = _htmlSanitizer.Sanitize(model.Description);
                        description = HttpUtility.HtmlDecode(description);

                        Movie movie = new Movie(
                            movieId: movieId,
                            title: title,
                            description: description,
                            price: model.Price,
                            userId: zalogowanyUser.Id,
                            categoryId: model.CategoryId
                            );

                        _context.Movies.Add(movie);
                        await _context.SaveChangesAsync();



                        // dodanie nowego zdjęcia
                        await CreateNewPhoto(model.Files, movieId);




                        returnResult.Success = true;
                        returnResult.Object = model;
                        returnResult.Message = "Nowy rekord został utworzony";

                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }
            return returnResult;
        }


        private string RemoveNbsp (string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            // Usuwanie &nbsp; z początku
            while (text.StartsWith("&nbsp;"))
            {
                text = text.Substring("&nbsp;".Length);
            }

            // Usuwanie &nbsp; z końca
            while (text.EndsWith("&nbsp;"))
            {
                text = text.Substring(0, text.Length - "&nbsp;".Length);
            }

            return text;
        }




        public async Task<ResultViewModel<EditMovieDto>> Update(EditMovieDto model)
        {
            var returnResult = new ResultViewModel<EditMovieDto>() { Success = false, Message = "", Object = new EditMovieDto() };

            if (model != null)
            {
                try
                {

                    var movie = await _context.Movies.FirstOrDefaultAsync(f => f.MovieId == model.MovieId);
                    if (movie != null)
                    {
                        /*object photoData = model.PhotoData == null ? movie.Photo : await ChangeFileToBytes(model.PhotoData);
                        byte[] photo = photoData as byte[];
                        */

                        string title = _htmlSanitizer.Sanitize(model.Title);
                        title = HttpUtility.HtmlDecode(title);

                        string description = _htmlSanitizer.Sanitize(model.Description);
                        description = HttpUtility.HtmlDecode(description);


                        movie.Update(
                            title: title,
                            description: description,
                            price: model.Price,
                            categoryId: model.CategoryId
                            );
                        _context.Entry(movie).State = EntityState.Modified;
                        await _context.SaveChangesAsync();



                        // dodanie kolejnych zdjęć
                        await CreateNewPhoto(model.Files, movie.MovieId);



                        returnResult.Success = true;
                        returnResult.Object = model;
                        returnResult.Message = "Dane zostały zaktualizowane";
                    }
                    else
                    {
                        returnResult.Message = "Movie was null";
                    }

                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Model was null";
            }
            return returnResult;
        }




        public async Task<ResultViewModel<bool>> Delete(string movieId)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(movieId))
            {
                try
                {
                    var movie = await _context.Movies.FirstOrDefaultAsync(f => f.MovieId == movieId);
                    if (movie != null)
                    {

                        var photosMovie = await _context.PhotosMovie.Where (w=> w.MovieId == movieId).ToListAsync ();
                        foreach (var photoMovie in photosMovie)
                            _context.PhotosMovie.Remove (photoMovie);


                        _context.Movies.Remove(movie);
                        await _context.SaveChangesAsync();

                        returnResult.Success = true;
                        returnResult.Object = true;
                    }
                    else
                    {
                        returnResult.Message = "Movie was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Id was null";
            }
            return returnResult;
        }







        public async Task<ResultViewModel<bool>> DeletePhotoMovie (string photoMovieId)
        {
            var returnResult = new ResultViewModel<bool>() { Success = false, Message = "", Object = false };

            if (!string.IsNullOrEmpty(photoMovieId))
            {
                try
                {
                    var photoMovie = await _context.PhotosMovie.FirstOrDefaultAsync (f=> f.PhotoMovieId == photoMovieId);
                    if (photoMovie != null)
                    {
                        _context.PhotosMovie.Remove(photoMovie);
                        await _context.SaveChangesAsync();

                        returnResult.Success = true;
                        returnResult.Object = true;
                    }
                    else
                    {
                        returnResult.Message = "Movie was null";
                    }
                }
                catch (Exception ex)
                {
                    returnResult.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                returnResult.Message = "Id was null";
            }
            return returnResult;
        }






        /// <summary>
        /// Zamienia zdjęcie na bytes
        /// </summary>
        private async Task CreateNewPhoto(List<IFormFile> files, string movieId)
        {
            try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            byte[] photoData;
                            using (var stream = new MemoryStream())
                            {
                                file.CopyTo(stream);
                                photoData = stream.ToArray();

                                PhotoMovie photoMovie = new PhotoMovie()
                                {
                                    PhotoMovieId = Guid.NewGuid().ToString(),
                                    PhotoData = photoData,
                                    MovieId = movieId
                                };
                                _context.PhotosMovie.Add (photoMovie);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch { }
        }



        public string RemoveHtmlTags(string input)
        {
            // Usuwamy treść między znacznikami < i >, w tym same znaczniki
            return Regex.Replace(input, "<[^>]*>", string.Empty);
        }




    }
}
