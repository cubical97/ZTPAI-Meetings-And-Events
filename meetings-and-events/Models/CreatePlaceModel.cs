using System;

namespace meetings_and_events.Models
{
    public class CreatePlaceModel
    {
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string[] TimeOC1 { get; set; }
        public string[] TimeOC2 { get; set; }
        public string[] TimeOC3 { get; set; }
        public string[] TimeOC4 { get; set; }
        public string[] TimeOC5 { get; set; }
        public string[] TimeOC6 { get; set; }
        public string[] TimeOC7 { get; set; }
    }
}