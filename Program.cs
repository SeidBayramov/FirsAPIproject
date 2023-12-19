using FirsAPIproject.DAL;
using FirsAPIproject.Repositories.Implementations;
using FirsAPIproject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FirsAPIproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")
            ));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBrandRepository,BrandRepository>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();

            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}