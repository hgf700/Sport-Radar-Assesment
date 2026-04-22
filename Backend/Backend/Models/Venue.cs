using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Venue
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(1)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }
}
