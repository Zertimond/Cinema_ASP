using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema_ASP.Models
{
    public class Person
    {
        [Key] public int PersonId { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string MiddleName { get; set; }

    }
}
