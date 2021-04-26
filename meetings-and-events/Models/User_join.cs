using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("User_join")]
    public class User_join
    {
        [Key]
        public int id_join { get; set; }
        
        [ForeignKey("User")]
        public int id_user { get; set; }
        [ForeignKey("Place")]
        public int id_place { get; set; }
    }
}