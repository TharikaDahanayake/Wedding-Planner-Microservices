using Microsoft.EntityFrameworkCore;
using TaskService.Models;

namespace TaskService.Data
{
    public class TaskDbContext_D_BIT_23_0030 : DbContext
    {
        public TaskDbContext_D_BIT_23_0030(DbContextOptions<TaskDbContext_D_BIT_23_0030> options)
        : base(options)
        {
        }

        // Add this DbSet
        public DbSet<Task_D_BIT_23_0030> Task_D_BIT_23_0030 { get; set; }
    }
}
