using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;
using NpgsqlTypes;

namespace meetings_and_events.Models
{
    [Table("Place_data_multitime")]
    public class Place_data_multitime
    {
        [Key]
        public int id_data { get; set; }
        [ForeignKey("Place")]
        public int id_place { get; set; }
        public E_day_week day_week { get; set; }
        public TimeSpan start_date { get; set; }
        public TimeSpan end_date { get; set; }
        
        public virtual Place Place { get; set; }
    }
}
