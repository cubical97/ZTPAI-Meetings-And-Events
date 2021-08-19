namespace meetings_and_events.Models
{
    public class ResultPlaceList
    {
        private int id_place;
        private string title;
        private string description;
        private string image;
        private string users_username;
        private string address_city;
        private int rate_likes;
        private int rate_dislikes;
        private bool multitime;

        public int Id_place
        {
            get => id_place;
            set => id_place = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string Image
        {
            get => image;
            set => image = value;
        }

        public string Users_username
        {
            get => users_username;
            set => users_username = value;
        }

        public string Address_city
        {
            get => address_city;
            set => address_city = value;
        }

        public int Rate_likes
        {
            get => rate_likes;
            set => rate_likes = value;
        }

        public int Rate_dislikes
        {
            get => rate_dislikes;
            set => rate_dislikes = value;
        }

        public bool Multitime
        {
            get => multitime;
            set => multitime = value;
        }
    }
}
