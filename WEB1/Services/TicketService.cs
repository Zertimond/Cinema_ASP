using Cinema_ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_ASP.Services;

public class TicketService : ITicketService
{
    private readonly AppContext _context;

    public TicketService(AppContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateTicket(int ShowId, int Place, int Cost)
    {
        var ticket = new Tickets
        {
            TicketId = Guid.NewGuid(),
            ShowId = ShowId,
            Place = Place,
            Cost = Cost
        };

        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket.TicketId;
    }

    public async Task DeleteTicket(Guid Ticketid)
    {
        if (!await _context.Tickets.AnyAsync(Ticket => Ticket.TicketId == Ticketid))
        {
            throw new Exception("User was not found");
        }

        var user = await _context.Tickets
            .FirstOrDefaultAsync(user => user.TicketId == Ticketid);
        _context.Tickets.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Tickets>> GetAllTickets()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<Tickets> GetTicket(Guid Ticketid)
    {
        if (!await _context.Tickets.AnyAsync(Ticket => Ticket.TicketId == Ticketid))
        {
            throw new Exception("User was not found");
        }

        var Ticket = await _context.Tickets
            .FirstOrDefaultAsync(Ticket => Ticket.TicketId == Ticketid);

        await _context.SaveChangesAsync();
        return Ticket;
    }

    public async Task<string> UpdateTicket(int Place, int Cost, Guid Ticketid)
    {
        if (!await _context.Tickets.AnyAsync(Ticket => Ticket.TicketId == Ticketid))
        {
            throw new Exception("User was not found");
        }

        var Ticket = await _context.Tickets
            .FirstOrDefaultAsync(Ticket => Ticket.TicketId == Ticketid);

        Ticket.Place = Place;
        Ticket.Cost = Cost;

        _context.Tickets.Update(Ticket);
        await _context.SaveChangesAsync();

        return $"User with {Ticket.TicketId} updated!";
    }
}
