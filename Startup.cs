using Application.Services;
using Application.Services.Abs;
using Ganss.Xss;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApplication71.Data;
using WebApplication71.Models;
using WebApplication71.Repos;
using WebApplication71.Repos.Abs;
using WebApplication71.Services;
using WebApplication71.Services.Abs;

namespace WebApplication71
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddCookie(cookie =>
                {
                    cookie.LoginPath = "/Account/Login";
                    cookie.AccessDeniedPath = "/Account/Login";
                    cookie.Cookie.HttpOnly = true;
                    cookie.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Ustal czas wygaœniêcia ciasteczka
                    cookie.SlidingExpiration = true; // Odnawiaj czas wygaœniêcia przy aktywnoœci
                });
            services.AddAuthorization();

            services.ConfigureApplicationCookie(cookie =>
            {
                cookie.LoginPath = "/Account/Login";
                cookie.AccessDeniedPath = "/Account/Login";
                cookie.Cookie.HttpOnly = true;
                cookie.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                cookie.SlidingExpiration = true;
            });


            services.AddControllersWithViews();
            services.AddHttpContextAccessor();


            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<ILogowaniaRepository, LogowaniaRepository>();

            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient (typeof (HtmlSanitizer));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
