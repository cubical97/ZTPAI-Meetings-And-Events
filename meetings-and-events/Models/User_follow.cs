using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("users_follow")]
    public class User_follow
    {
        [Key]
        public int id_follow { get; set; }
        public int id_user { get; set; }
        public int id_place { get; set; }
    }
}