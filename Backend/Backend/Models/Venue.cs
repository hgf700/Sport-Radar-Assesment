using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Venue
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
}
