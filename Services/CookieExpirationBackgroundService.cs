using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WebApplication71.Data;
using WebApplication71.Models;

namespace WebApplication71.Services
{
    public class CookieExpirationBackgroundService : BackgroundService
    {
        private readonly ILogger<CookieExpirationBackgroundService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public CookieExpirationBackgroundService(ILogger<CookieExpirationBackgroundService> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Wstrzymaj na chwilę (np. 1 sekunda)
                await Task.Delay(1000, stoppingToken);

                // Tutaj sprawdź, czy ciasteczko wygasło, np. z bazy danych
                var cookieExpired = CheckIfCookieExpired();
                if (cookieExpired)
                {
                    // Jeśli ciasteczko wygasło, wykonaj potrzebną akcję
                    await UpdateRecordAsync();
                }
            }
        }

        private bool CheckIfCookieExpired()
        {
            // Sprawdzanie, czy ciasteczko wygasło
            return true;  // Przykład: powróć true, jeśli ciasteczko wygasło
        }

        private async Task UpdateRecordAsync()
        { 
            _logger.LogInformation("Rekord zaktualizowany.");

            _context.Roles.Add (new ApplicationRole ("role1"));
            await _context.SaveChangesAsync ();
        }
    }

}
