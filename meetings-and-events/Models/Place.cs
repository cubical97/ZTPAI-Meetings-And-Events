using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace meetings_and_events.Models
{
    [Table("Place")]
    public class Place
    {
        [Key, Required]
        public int id_place { get; set; }
        [MaxLength(128), Required]
        public string title { get; set; }
        public string description { get; set; }
        [ForeignKey("User"), Required]
        public int id_user { get; set; }
        [MaxLength(256)]
        public string image { get; set; }
        [Required]
        public DateTime create_date { get; set; }
        [Required]
        public bool multi_time { get; set; }
        
        public virtual Place_data_onetime Place_data_onetime { get; set; }
        public virtual Place_address Place_address { get; set; }
        
        public virtual ICollection<Place_data_multitime> Place_data_multitime { get; set; }
        public virtual ICollection<Place_special_close> Place_special_close { get; set; }
        
        public virtual ICollection<User_follow> User_follow { get; set; }
        public virtual ICollection<User_join> User_join { get; set; }
        public virtual ICollection<Place_rate> Place_rate { get; set; }
        public virtual ICollection<Place_comments> Place_comments { get; set; }
    }
}