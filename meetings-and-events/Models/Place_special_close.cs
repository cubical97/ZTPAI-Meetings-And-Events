using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Place_special_close")]
    public class Place_special_close
    {
        [Key]
        public int id_data_closetime { get; set; }
        public DateTime date { get; set; }
        
        [ForeignKey("Place")]
        public int id_place { get; set; }
        
        public virtual Place Place { get; set; }
    }
}