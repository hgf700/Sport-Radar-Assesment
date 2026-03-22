using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Event
{
    [Key]
    public int Id { get; set; }
    public string NameOfDay { get; set; }
    public DateTime Date {  get; set; }
    public TimeOnly Time {  get; set; }
    public int SportId { get; set; }
    public Sport Sport { get; set; }
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; }
    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; }
}
