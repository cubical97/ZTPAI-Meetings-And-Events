using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace meetings_and_events.Models
{
    [Table("Place_comments")]
    public class Place_comments
    {
        [Key, Required]
        public int id_comment { get; set; }
        [Required]
        public DateTime comment_date { get; set; }
        [Required]
        public string comment { get; set; }
        
        [ForeignKey("User"), Required]
        public int id_user { get; set; }
        [ForeignKey("Place"), Required]
        public int id_place { get; set; }
    }
}