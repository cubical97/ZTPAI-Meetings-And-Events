using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Place")]
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
        
        public virtual Place_data_onetime Place_data_onetime { get; set; }
        public virtual Place_data_multitime Place_data_multitime { get; set; }
        public virtual Place_address Place_address { get; set; }
        
        public virtual ICollection<Place_special_close> Place_special_close { get; set; }
        public virtual ICollection<User_follow> User_follow { get; set; }
        public virtual ICollection<User_join> User_join { get; set; }
        public virtual ICollection<Place_rate> Place_rate { get; set; }
        public virtual ICollection<Place_comments> Place_comments { get; set; }
    }
}