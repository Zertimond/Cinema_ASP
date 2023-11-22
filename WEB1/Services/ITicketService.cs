using Cinema_ASP.Models;

namespace Cinema_ASP.Services;

public interface ITicketService
{
    Task<Tickets> GetTicket(int Ticketid);
    Task<IEnumerable<Tickets>> GetAllTickets();
    Task<int> CreateTicket(int Ticketid, int ShowId, int Place, int Cost);
    Task<string> UpdateTicket(int Place, int Cost, int Ticketid);
    Task DeleteTicket(int Ticketid);
}
