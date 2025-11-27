namespace Cinema.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public Session? Session { get; set; }
        public int SeatNumber { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime PurchaseTime { get; set; }
    }
}