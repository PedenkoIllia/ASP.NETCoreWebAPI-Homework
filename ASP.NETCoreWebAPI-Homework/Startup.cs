using ASP.NETCoreWebAPI_Homework.Data.Context;
using ASP.NETCoreWebAPI_Homework.Logic.MapperConfigurations;
using ASP.NETCoreWebAPI_Homework.Logic.Services;
using ASP.NETCoreWebAPI_Homework.Logic.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASP.NETCoreWebAPI_Homework
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
            services.AddControllers();

            services.AddDbContext<ApplicationDBContext>(option =>
                option.UseSqlite("Data Source=.HW_ASP_Pedenko.BookCatalog"));

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }).CreateMapper());

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBookService, BookService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
