using System.ComponentModel.DataAnnotations;

namespace GuestService.Models
{
    public class Guest_D_BIT_23_0030
    {
        [Key]
        public int GuestId { get; set; }

        public int EventId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string RSVP { get; set; }
    }
}
