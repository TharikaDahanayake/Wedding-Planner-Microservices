using Microsoft.EntityFrameworkCore;
using EventService.Data;
using EventService.Models;


namespace EventService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add In-Memory Database
            builder.Services.AddDbContext<EventDbContext_D_BIT_23_0030>(options =>
                options.UseInMemoryDatabase("EventDb_D_BIT_23_0030"));

            // Add controllers
            builder.Services.AddControllers();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enable Swagger UI at root
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service D_BIT_23_0030 v1");
                c.RoutePrefix = string.Empty; // Swagger opens at http://localhost:5001
            });

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EventDbContext_D_BIT_23_0030>();
                db.Database.EnsureCreated();
            }

            // Map controllers
            app.MapControllers();

            // Run EventService on port 5001
            app.Run("http://localhost:5001");
        }
    }
}
