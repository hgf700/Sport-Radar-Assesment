using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

[Index(nameof(NameOfTeam), IsUnique = true)]
public class Team
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(1)]
    public string NameOfTeam { get; set; }

    [MaxLength(300)]
    public string? TeamInformation { get; set; }
}
