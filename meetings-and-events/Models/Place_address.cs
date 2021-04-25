using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class Place_address
    {
        [Key]
        public int id_address { get; set; }
        public int id_place { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string number { get; set; }
    }
}