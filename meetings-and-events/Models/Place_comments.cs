using System;
using System.ComponentModel.DataAnnotations;

namespace meetings_and_events.Models
{
    public class Place_comments
    {
        [Key]
        public int id_comment { get; set; }
        public int id_place { get; set; }
        public int id_user { get; set; }
        public DateTime comment_date { get; set; }
        public string comment { get; set; }
    }
}