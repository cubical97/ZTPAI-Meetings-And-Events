namespace meetings_and_events.Models.PlaceEdit
{
    public class EditDateMeetingModel
    {
        public int place_id { get; set; }
        public DatepickerModel datepicker { get; set; }
        public string[] timeOC { get; set; }
    }
}
