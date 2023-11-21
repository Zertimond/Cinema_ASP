namespace Cinema_ASP.Models
{
    public class TicketUpdateModel
    {
        public Guid Id { get; set; }
        public int ShowId { get; set; }
        public int Place { get; set; }
        public int Cost { get; set; }
    }
}
