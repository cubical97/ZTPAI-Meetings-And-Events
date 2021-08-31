using System.Reflection.Metadata;

namespace meetings_and_events.Models.PlaceEdit
{
    public class EditImageModel
    {
        public int place_id { get; set; }

        public Blob myImage { get; set; }
    }
}
