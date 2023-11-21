using Microsoft.EntityFrameworkCore;
using System;
using Cinema_ASP.Models;
using Cinema_ASP.Services;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
builder.Services.AddDbContext<Cinema_ASP.AppContext>(options => options.UseSqlServer(connection));

var services = builder.Services;

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

/*app.MapGet("/", (Cinema_ASP.AppContext db) => db.Tickets.ToList());


app.MapGet("/ticketsbyid/{TicketId}", async (Guid TicketId, Cinema_ASP.AppContext db) =>
{
    Tickets? tickets = await db.Tickets.FirstOrDefaultAsync(u => u.TicketId == TicketId);
    if (tickets == null)
    {
        return Results.NotFound(new { message = "Квиток за ID не знайдено" });
    }
    return Results.Json(tickets);
});

app.MapGet("/ticketsbyshow/{ShowId}", async (int ShowId, Cinema_ASP.AppContext db) =>
{
    var tickets = await db.Tickets.Where(u => u.ShowId == ShowId).ToListAsync();
    if (tickets == null)
    {
        return Results.NotFound(new { message = "Квиток за номером показу не знайдено" });
    }
    return Results.Json(tickets);
});


app.MapPost("/addtickets", (Guid TicketId, int ShowId, int Place, int Cost, Cinema_ASP.AppContext db) =>
{
    Tickets tickets = new Tickets();
    tickets.TicketId = TicketId;
    tickets.ShowId = ShowId;
    tickets.Place = Place;
    tickets.Cost = Cost;
    db.Tickets.Add(tickets);
    db.SaveChanges();
    return tickets;
});*/

app.Run(async (context) =>
{
    var ticketService = context.RequestServices.GetService<ITicketService>()
                       ?? throw new Exception("Ticket service not found");
    var path = context.Request.Path;
    switch (path)
    {
        case "/tickets":
            {
                var tickets = await ticketService.GetAllTickets();
                await context.Response.WriteAsJsonAsync(tickets);
                break;
            }
        case "/tickets/create":
            {
                var ShowId = await context.Request.ReadFromJsonAsync<int>();
                var Place = await context.Request.ReadFromJsonAsync<int>();
                var Cost = await context.Request.ReadFromJsonAsync<int>();
                var userId = await ticketService.CreateTicket(ShowId, Place,  Cost);
                await context.Response.WriteAsJsonAsync(userId);
                break;
            }
        case "/tickets/update":
            {
                var model = await context.Request.ReadFromJsonAsync<TicketUpdateModel>()
                            ?? throw new Exception("Invalid ticket model");
                await ticketService.UpdateTicket(model.Place, model.Cost, model.Id);
                await context.Response.WriteAsync("Ticket updated");
                break;
            }
        case "/tickets/delete":
            {
                var model = await context.Request.ReadFromJsonAsync<Guid>();
                await ticketService.DeleteTicket(model);
                await context.Response.WriteAsync("Ticket deleted");
                break;
            }
    }
});

app.Run();