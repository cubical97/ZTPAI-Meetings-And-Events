using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Npgsql;
using NpgsqlTypes;

namespace meetings_and_events.Models
{
    [Table("Place_data_multitime")]
    public class Place_data_multitime
    {
        [Key, Required]
        public int id_data { get; set; }
        [ForeignKey("Place"), Required]
        public int id_place { get; set; }
        [Required]
        public E_day_week day_week { get; set; }
        [Required]
        public TimeSpan start_date { get; set; }
        [Required]
        public TimeSpan end_date { get; set; }
        
        public virtual Place Place { get; set; }
    }
}
