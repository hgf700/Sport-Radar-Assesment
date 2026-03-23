using Backend.DB;
using Backend.Models;
using Backend.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Controllers;

[ApiController]
[Route("event")]
public class EventController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context
        )
    {
        _context= context;
    }

    [HttpGet("show-events")]
    public async Task<ActionResult<getEventsDto>> ShowEvents()
    {
        var ev = await _context.Events
           .Include(e => e.Sport)
           .Include(e => e.HomeTeam)
           .Include(e => e.AwayTeam)
           .Include(e => e.Venue)
           .ToListAsync();

        if (ev == null)
            return NotFound();

        var eventsDto = ev.Select(ev => new getEventsDto
        {
            id = ev.Id,
            dateTime = ev.DateTime,
            description = ev.Description,
            sportName = ev.Sport.SportName,
            homeTeamName = ev.HomeTeam.NameOfTeam,
            awayTeamName = ev.AwayTeam.NameOfTeam,
            venueName = ev.Venue.Name,
            venueCity = ev.Venue.City
        }).ToList();

        return Ok(eventsDto);
    }

    [HttpGet("show-selected-event/{eventId}")]
    public async Task<ActionResult<getSelectedEventDto>>ShowSelectedEvent(int eventId)
    {
        var ev = await _context.Events
            .Include(e => e.Sport)
            .Include(e => e.HomeTeam)
            .Include(e => e.AwayTeam)
            .Include(e => e.Venue)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        if (ev == null)
            return NotFound();

        var dto = new getSelectedEventDto
        {
            id = ev.Id,
            dateTime = ev.DateTime,
            description = ev.Description,
            sportName = ev.Sport.SportName,
            homeTeamName = ev.HomeTeam.NameOfTeam,
            awayTeamName = ev.AwayTeam.NameOfTeam,
            venueName = ev.Venue.Name,
            venueCity = ev.Venue.City
        };

        return dto;
    }

    [HttpPost("create-new-event")]
    public async Task<IActionResult> CreateNewEvent([FromBody] postCreateEventDto dto)
    {
         var newevent = new Event
         {
             DateTime=dto.dateTime,
             Description= dto.description,


         };

        _context.Events.Add(newevent);

        await _context.SaveChangesAsync();

        return Ok();
    }
    
}
