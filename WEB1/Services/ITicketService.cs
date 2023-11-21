using Cinema_ASP.Models;

namespace Cinema_ASP.Services;

public interface ITicketService
{
    Task<Tickets> GetTicket(Guid Ticketid);
    Task<IEnumerable<Tickets>> GetAllTickets();
    Task<Guid> CreateTicket(int ShowId, int Place, int Cost);
    Task<string> UpdateTicket(int Place, int Cost, Guid Ticketid);
    Task DeleteTicket(Guid Ticketid);
}
