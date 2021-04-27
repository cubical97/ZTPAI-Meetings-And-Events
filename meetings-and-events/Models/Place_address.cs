using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Place_address")]
    public class Place_address
    {
        [Key]
        public int id_address { get; set; }
        [ForeignKey("Place")]
        public int id_place { get; set; }
        [MaxLength(64)]
        public string country { get; set; }
        [MaxLength(64)]
        public string city { get; set; }
        [MaxLength(64)]
        public string street { get; set; }
        [MaxLength(16)]
        public string number { get; set; }
        
        public virtual Place Place { get; set; }
    }
}