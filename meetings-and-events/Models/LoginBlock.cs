namespace meetings_and_events.Models
{
    public class LoginBlock
    {
        private bool logged = false;
        private string errorMessage = null;
        private int idUser = -1;
        private string username = null;

        public bool Logged { get => logged; set => logged = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public string Username { get => username; set => username = value; }
    }
}