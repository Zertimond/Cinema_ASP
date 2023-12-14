using System.ComponentModel.DataAnnotations;

namespace Lab1_Data.Entities;

public class Gender
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(10)]
    public string Name { get; set; } = null!;

    public List<User>? Users { get; set; } = new();
}