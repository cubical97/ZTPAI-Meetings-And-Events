using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace meetings_and_events.Models
{
    [Table("Users_follow")]
    public class User_follow
    {
        [Key, Required]
        public int id_follow { get; set; }
        
        [ForeignKey("User"), Required]
        public int id_user { get; set; }
        [ForeignKey("Place"), Required]
        public int id_place { get; set; }
    }
}