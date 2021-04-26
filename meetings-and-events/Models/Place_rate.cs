using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Place_rate")]
    public class Place_rate
    {
        [Key]
        public int id_rate { get; set; }
        public bool like { get; set; }
        
        [ForeignKey("User")]
        public int id_user { get; set; }
        [ForeignKey("Place")]
        public int id_place { get; set; }
    }
}