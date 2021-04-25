using System;
using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class Place_special_close
    {
        [Key]
        public int id_data_closetime { get; set; }
        public int id_place { get; set; }

        public DateTime date { get; set; }
    }
}