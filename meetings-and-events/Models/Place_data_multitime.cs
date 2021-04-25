using System;
using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class Place_data_multitime
    {
        [Key]
        public int id_data_multitime { get; set; }
        public int id_place { get; set; }
        public enum Day_week
        {
            MONDAY,
            TUESDAY,
            WEDNESDAY,
            THURSDAY,
            FRIDAY,
            SATURDAY,
            SUNDAY
        }
        public TimeSpan start_date { get; set; }
        public TimeSpan end_date { get; set; }
    }
}