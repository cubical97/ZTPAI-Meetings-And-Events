using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Place_data_onetime")]
    public class Place_data_onetime
    {
        [Key]
        public int id_data { get; set; }
        [ForeignKey("Place")]
        public int id_place { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        
        public virtual Place Place { get; set; }
    }
}