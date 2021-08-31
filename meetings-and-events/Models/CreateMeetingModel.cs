using System;

namespace meetings_and_events.Models
{
    public class CreateMeetingModel
    {
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public DatepickerModel Datepicker { get; set; }
        public string[] TimeOC { get; set; }
    }
}