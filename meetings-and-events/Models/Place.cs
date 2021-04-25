using System;
using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class Place
    {
        [Key]
        public int id_place { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int id_user { get; set; }
        public string image { get; set; }
        public DateTime create_date { get; set; }
        public bool multi_time { get; set; }
    }
}