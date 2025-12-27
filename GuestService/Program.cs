
using GuestService.Data;
using GuestService.Services;
using Microsoft.EntityFrameworkCore;


namespace GuestService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<GuestDbContext_D_BIT_23_0030>(options =>
                options.UseSqlite("Data Source=guests_D_BIT_23_0030.db"));

            // Register HTTP client for Event Service
            builder.Services.AddHttpClient<IEventClient_D_BIT_23_0030, EventClient_D_BIT_23_0030>(c =>
            {
                c.BaseAddress = new Uri("http://localhost:5001");
            });

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Guest Service D_BIT_23_0030 v1");
                c.RoutePrefix = string.Empty; // Swagger opens at http://localhost:5002
            });

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<GuestDbContext_D_BIT_23_0030>();
                db.Database.EnsureCreated();
            }

            // Map controllers
            app.MapControllers();

            // Run GuestService on port 5002
            app.Run("http://localhost:5002");
        }
    }
}
