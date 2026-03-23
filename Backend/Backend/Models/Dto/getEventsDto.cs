namespace Backend.Models.Dto;

public class getEventsDto
{
    public int id { get; set; }
    public DateTime dateTime { get; set; }
    public string description { get; set; }
    public SportName sportName { get; set; }
    public string homeTeamName { get; set; }
    public string awayTeamName { get; set; }
    public string venueName { get; set; }
    public string venueCity { get; set; }
}
