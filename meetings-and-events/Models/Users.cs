using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetings_and_events.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int id_user { get; set; }
        [MaxLength(64)]
        public string username { get; set; }
        [MaxLength(256)]
        public byte[] password { get; set; }
        [MaxLength(128)]
        public string email { get; set; }
        public DateTime create_date { get; set; }
        
        public virtual ICollection<User_follow> User_follow { get; set; }
        public virtual ICollection<User_join> User_join { get; set; }
        public virtual ICollection<Place_rate> Place_rate { get; set; }
        public virtual ICollection<Place_comments> Place_comments { get; set; }
    }
}