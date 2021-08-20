namespace meetings_and_events.Models
{
    public class ResultPlaceListComment
    {
        private int comment_id;
        private string author;
        private string createdate;
        private string comment;

        public int Comment_id
        {
            get => comment_id;
            set => comment_id = value;
        }

        public string Author
        {
            get => author;
            set => author = value;
        }

        public string Createdate
        {
            get => createdate;
            set => createdate = value;
        }

        public string Comment
        {
            get => comment;
            set => comment = value;
        }
    }
}
