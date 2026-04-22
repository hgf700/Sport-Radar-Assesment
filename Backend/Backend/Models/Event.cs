using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

[Index(nameof(_SportId))]
[Index(nameof(_VenueId))]
[Index(nameof(_HomeTeamId))]
[Index(nameof(_AwayTeamId))]
public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DateTime { get; set; }

    [MaxLength(300)]
    public string? Description { get; set; }
    
    [Required]
    public int _SportId { get; set; }
    public Sport? Sport { get; set; }

    [Required]
    public int _HomeTeamId { get; set; }
    public Team? HomeTeam { get; set; }

    [Required]
    public int _AwayTeamId { get; set; }
    public Team? AwayTeam { get; set; }

    [Required]
    public int _VenueId { get; set;  }
    public Venue? Venue { get; set; }
}
