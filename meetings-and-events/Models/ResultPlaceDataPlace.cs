namespace meetings_and_events.Models
{
    public class ResultPlaceDataPlace
    {
        public string mo { get; set; }
        public string tu { get; set; }
        public string we { get; set; }
        public string th { get; set; }
        public string fr { get; set; }
        public string sat { get; set; }
        public string sun { get; set; }
        public string[] closed { get; set; }

        public ResultPlaceDataPlace()
        {
            this.mo = "closed";
            this.tu = "closed";
            this.we = "closed";
            this.th = "closed";
            this.fr = "closed";
            this.sat = "closed";
            this.sun = "closed";
            this.closed = null;
        }
    }
}
