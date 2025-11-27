namespace Cinema.API.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}