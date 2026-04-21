using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Dto;

public class postCreateEventDto
{
    [Required]
    public DateTime dateTime { get; set; }

    [MaxLength(300)]
    public string? description { get; set; }

    [Required]
    public SportName sportName { get; set; }

    [Required]
    [MaxLength(100)]
    public string homeTeamName { get; set; }

    [Required]
    [MaxLength(100)]
    public string awayTeamName { get; set; }

    [Required]
    [MaxLength(100)]
    public string venueName { get; set; }

    [Required]
    [MaxLength(100)]
    public string venueCity { get; set; }
}