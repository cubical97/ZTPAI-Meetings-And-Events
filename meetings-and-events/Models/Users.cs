using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int id_user { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}