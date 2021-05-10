using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace meetings_and_events.Models
{
    [Table("User_join")]
    public class User_join
    {
        [Key, Required]
        public int id_join { get; set; }
        
        [ForeignKey("User"), Required]
        public int id_user { get; set; }
        [ForeignKey("Place"), Required]
        public int id_place { get; set; }
    }
}