using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Sport
{
    [Key]
    public int Id { get; set; }
    public string SportName { get; set; }
}
