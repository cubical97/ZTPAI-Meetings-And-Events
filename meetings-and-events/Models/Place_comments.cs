using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Place_comments")]
    public class Place_comments
    {
        [Key]
        public int id_comment { get; set; }
        public DateTime comment_date { get; set; }
        public string comment { get; set; }
        
        [ForeignKey("User")]
        public int id_user { get; set; }
        [ForeignKey("Place")]
        public int id_place { get; set; }
    }
}