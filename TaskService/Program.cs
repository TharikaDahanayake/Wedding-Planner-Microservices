
using TaskService.Data;
using Microsoft.EntityFrameworkCore;
using TaskService.Services;


namespace TaskService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<TaskDbContext_D_BIT_23_0030>(options =>
                options.UseSqlite("Data Source=tasks_D_BIT_23_0030.db"));

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

            // Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Service D_BIT_23_0030 v1");
                c.RoutePrefix = string.Empty; // Makes Swagger open at http://localhost:5003/
            });

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TaskDbContext_D_BIT_23_0030>();
                db.Database.EnsureCreated();
            }

            // Map controllers
            app.MapControllers();

            // Run service on port 5003
            app.Run("http://localhost:5003");
        }
    }
}
