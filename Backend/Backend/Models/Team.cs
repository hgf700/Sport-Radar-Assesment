using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Team
{
    [Key]
    public int Id { get; set; }
    public string NameOfTeam { get; set; }
    public string? TeamInformation { get; set; }
}
