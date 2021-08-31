using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace meetings_and_events.Models
{
    [Table("Place_data_onetime")]
    public class Place_data_onetime
    {
        [Key, Required]
        public int id_data { get; set; }
        [ForeignKey("Place"), Required]
        public int id_place { get; set; }
        [Required]
        public DateTime start_date { get; set; }
        [Required]
        public DateTime end_date { get; set; }
        
        public virtual Place Place { get; set; }
    }
}