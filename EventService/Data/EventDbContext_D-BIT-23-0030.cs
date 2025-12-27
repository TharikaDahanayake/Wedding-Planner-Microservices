using Microsoft.EntityFrameworkCore;
using EventService.Models;

namespace EventService.Data
{
    public class EventDbContext_D_BIT_23_0030 : DbContext
    {
        internal readonly object Event_D_BIT_23_0030;

        public EventDbContext_D_BIT_23_0030(DbContextOptions<EventDbContext_D_BIT_23_0030> options)
            : base(options)
        {
        }

        public DbSet<Event_D_BIT_23_0030> Events { get; set; }
    }
}
