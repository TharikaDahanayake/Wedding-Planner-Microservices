using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class Event_D_BIT_23_0030
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Venue { get; set; } = string.Empty;
    }
}
