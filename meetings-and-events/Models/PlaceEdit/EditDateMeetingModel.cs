using System;

namespace meetings_and_events.Models.PlaceEdit
{
    public class EditDateMeetingModel
    {
        public string timeopen { get; set; }
        public string timeclose { get; set; }
        public int place_id { get; set; }
        public DatepickerModel datepicker { get; set; }
    }
}
