using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class Place_rate
    {
        [Key]
        public int id_rate { get; set; }
        public int id_place { get; set; }
        public int id_user { get; set; }
        public bool like { get; set; }
    }
}