using Microsoft.AspNetCore.Routing.Matching;

namespace meetings_and_events
{
    public class User
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string pass { get; set; }
        public string description { get; set; }
    }
}