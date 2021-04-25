using System;
using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class Place_data_onetime
    {
        [Key]
        public int id_data_onetime { get; set; }
        public int id_place { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}