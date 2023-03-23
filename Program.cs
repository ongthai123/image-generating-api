using Image_Generating_APIs.Interfaces;
using Image_Generating_APIs.Models;
using Image_Generating_APIs.Services;

namespace Image_Generating_APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

            builder.Services.AddTransient<IMongoDBService, MongoDBService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy-public", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .Build();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("CorsPolicy-public");

            app.MapControllers();

            app.Run();
        }
    }
}