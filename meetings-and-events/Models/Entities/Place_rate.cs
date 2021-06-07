using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace meetings_and_events.Models
{
    [Table("Place_rate")]
    public class Place_rate
    {
        [Key, Required]
        public int id_rate { get; set; }
        public bool like { get; set; }
        
        [ForeignKey("Users"), Required]
        public int id_user { get; set; }
        [ForeignKey("Place"), Required]
        public int id_place { get; set; }
        
        public virtual Place Place { get; set; }
        public virtual Users Users { get; set; }
    }
}