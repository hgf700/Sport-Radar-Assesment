using System.ComponentModel.DataAnnotations;

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

    //moge po ogarniac to
    // [Range(1, 100)]
    // public int PageSize { get; set; } = 10;

    // [Range(1, int.MaxValue)]
    // public int Page { get; set; } = 1;
}