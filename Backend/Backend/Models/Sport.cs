using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

[Index(nameof(SportName), IsUnique = true)]
public class Sport
{
    [Key]
    public int Id { get; set; }

    [Required]
    public SportName SportName { get; set; }
}

public enum SportName
{
    Football= 0,
    Ice_Hockey= 1,
}