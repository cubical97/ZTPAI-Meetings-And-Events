using System;

namespace meetings_and_events.Models
{
    public class CreateCommentModel
    {
        public int PlaceID { get; set; }
        public string CommentText { get; set; }
    }
}