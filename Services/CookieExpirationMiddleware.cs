using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication71.Data;
using WebApplication71.Models;

namespace WebApplication71.Services
{
    public class CookieExpirationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CookieExpirationMiddleware> _logger;

        public CookieExpirationMiddleware(RequestDelegate next, ILogger<CookieExpirationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cookieName = ".AspNetCore.Cookies"; // Domyślna nazwa ciasteczka w ASP.NET Core
            var cookieValue = context.Request.Cookies[cookieName];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                // Tutaj możesz dodać logikę sprawdzania czasu wygaśnięcia ciasteczka
                var cookieExpirationTime = DateTime.UtcNow.AddSeconds(5);  // Przykład, dostosuj do własnych potrzeb

                if (DateTime.UtcNow > cookieExpirationTime)
                {
                    // Cookie wygasło, wykonaj akcję, np. zaktualizuj rekord w bazie danych
                    await UpdateRecordAsync();
                }
            }

            // Przekazuje żądanie do następnego middleware w potoku
            await _next(context);
        }

        private async Task UpdateRecordAsync()
        {
            // Zaktualizowanie rekordu w bazie danych
            _logger.LogInformation("Rekord zaktualizowany po wygaśnięciu ciasteczka.");

            /*_context.Roles.Add(new ApplicationRole("role1"));
            await _context.SaveChangesAsync();*/
        }
    }

}
