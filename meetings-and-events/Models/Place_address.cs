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
        public string country { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        
        public virtual Place Place { get; set; }
    }
}