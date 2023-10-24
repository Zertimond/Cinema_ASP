using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema_ASP.Models
{
    public class Tickets
    {
        [Key] public int TicketId { get; set; }

        [Required] public int ShowId { get; set; }

        public int Place { get; set; }

        public int Cost { get; set; }

    }
}
