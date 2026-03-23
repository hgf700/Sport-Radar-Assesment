using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Sport
{
    [Key]
    public int Id { get; set; }
    public SportName SportName { get; set; }
}

public enum SportName
{
    Football= 0,
    Ice_Hockey= 1,
}