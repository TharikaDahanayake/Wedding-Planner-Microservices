using System.ComponentModel.DataAnnotations;

namespace TaskService.Models
{
    public class Task_D_BIT_23_0030
    {
        [Key]
        public int TaskId { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
