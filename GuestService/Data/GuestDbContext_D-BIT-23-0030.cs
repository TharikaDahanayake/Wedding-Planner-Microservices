using GuestService.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestService.Data
{
    public class GuestDbContext_D_BIT_23_0030 : DbContext
    {
        public GuestDbContext_D_BIT_23_0030(DbContextOptions<GuestDbContext_D_BIT_23_0030> opts) : base(opts) { }
        public DbSet<Guest_D_BIT_23_0030> Guest_D_BIT_23_0030 { get; set; }
    }
}
