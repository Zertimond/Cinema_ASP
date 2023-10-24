using Microsoft.EntityFrameworkCore;
using System;
using Cinema_ASP.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
builder.Services.AddDbContext<Cinema_ASP.AppContext>(options => options.UseSqlServer(connection));

var services = builder.Services;

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/", (Cinema_ASP.AppContext db) => db.Tickets.ToList());


app.MapGet("/ticketsbyid/{TicketId}", async (int TicketId, Cinema_ASP.AppContext db) =>
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


app.MapPost("/addtickets", (int TicketId, int ShowId, int Place, int Cost, Cinema_ASP.AppContext db) =>
{
    Tickets tickets = new Tickets();
    tickets.TicketId = TicketId;
    tickets.ShowId = ShowId;
    tickets.Place = Place;
    tickets.Cost = Cost;
    db.Tickets.Add(tickets);
    db.SaveChanges();
    return tickets;
});

app.Run();