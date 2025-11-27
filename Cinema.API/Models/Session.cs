namespace Cinema.API.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int HallId { get; set; }
        public Hall? Hall { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }
    }
}