using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace meetings_and_events.Models
{
    [Table("Place_special_close")]
    public class Place_special_close
    {
        [Key, Required]
        public int id_data_closetime { get; set; }
        [Required]
        public DateTime date { get; set; }
        
        [ForeignKey("Place"), Required]
        public int id_place { get; set; }
        
        public virtual Place Place { get; set; }
    }
}