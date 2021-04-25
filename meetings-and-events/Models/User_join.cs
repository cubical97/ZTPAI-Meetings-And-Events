using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class User_join
    {
        [Key]
        public int id_join { get; set; }
        public int id_user { get; set; }
        public int id_place { get; set; }
    }
}