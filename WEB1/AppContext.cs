using Cinema_ASP.Models;
using Microsoft.EntityFrameworkCore;
 
namespace Cinema_ASP
{
    public class AppContext : DbContext
    {
        public DbSet<Tickets> Tickets { get; set; } = null!;
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Tickets>().HasData(
                    new Tickets { TicketId = 1, ShowId = 4, Place = 15, Cost = 135 },
                    new Tickets { TicketId = 2, ShowId = 4, Place = 20, Cost = 140 },
                    new Tickets { TicketId = 3, ShowId = 4, Place = 5, Cost = 135 }
            );*/
        }
    }
}
