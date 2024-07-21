using NetTopologySuite;
using LocationApp.Business;
using LocationApp.Business.Contracts;
using LocationApp.DataAccess.Abstract;
using LocationApp.DataAccess.Concrete;
using LocationApp.DataAccess.UnitOfWork;
using LocationApp.Utilities.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;



namespace LocationApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
                 });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LocationContext>(options => options.UseNpgsql(
                builder.Configuration.GetConnectionString("PostgresConnection"), x => x.UseNetTopologySuite()));

            builder.Services.AddScoped<ICoordinateService,CoordinateManager>();
            builder.Services.AddScoped<ICoordinateRepository, CoordinateRepository>();
            builder.Services.AddScoped<ILineService, GeolocManager>();
            builder.Services.AddScoped<IGeolocRepository, GeolocRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.MapControllers();

            app.Run();
        }
    }
}
