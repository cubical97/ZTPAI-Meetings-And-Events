using System;
using System.Collections.Generic;

namespace meetings_and_events
{
    public class Event
    {
        public string title { get; set; }
        public string image { get; set; }
        
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public string description { get; set; }
        
        public string address { get; set; }
        public string address_2 { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        
        public int stars_1 { get; set; }
        public int stars_2 { get; set; }
        public int stars_3 { get; set; }
        public int stars_4 { get; set; }
        public int stars_5 { get; set; }

        public int stars_summary => (stars_1 + 2 * stars_2 + 3 * stars_3 + 4 * stars_4 + 5 * stars_5) * 10 /
                                    (stars_1 + stars_2 + stars_3 + stars_4 + stars_5);
        public List<Tuple<int,string>> comments { get; set; } // Comments (int author_id, string comment)

        public void AddComment(int author_id, string comment)
        {
            this.comments.Add(new Tuple<int, string>(author_id, comment));
        }
    }
}