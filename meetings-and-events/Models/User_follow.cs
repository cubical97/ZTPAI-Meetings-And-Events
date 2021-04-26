using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Users_follow")]
    public class User_follow
    {
        [Key]
        public int id_follow { get; set; }
        
        [ForeignKey("User")]
        public int id_user { get; set; }
        [ForeignKey("Place")]
        public int id_place { get; set; }
    }
}