using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Event
{
    [Key]
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }
    public int SportId { get; set; }
    public Sport Sport { get; set; }
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; }
    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; }
    public int VenueId { get; set;  }
    public Venue Venue { get; set; }
}
