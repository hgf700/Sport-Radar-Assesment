using Backend.DB;
using Backend.Models;
using Backend.Models.Dto;
using Backend.Patterns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Controllers;

[ApiController]
[Route("event")]
public class EventController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly RetryService _retry;

    public EventController(ApplicationDbContext context,
        RetryService retry
        )
    {
        _context= context;
        _retry = retry;
    }
    
    [HttpGet("show-events")]
    [EnableRateLimiting("RateLimitGet")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [EnableRateLimiting("RateLimitGet")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<getSelectedEventDto>>ShowSelectedEvent(int eventId)
    {
        if (eventId < 1) 
            return BadRequest();

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

    private string NormalizeEvent(string? value)
    {
        return new Pipe()
            .Add(new TrimFilter())
            .Add(new ToLowerInvariantFilter())
            .Add(new WhitespacesFilter())
            .Execute(new StringContext { Value = value })
            .Value!;
    }
    
    [HttpPost("create-new-event")]
    [EnableRateLimiting("RateLimitPost")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateNewEvent([FromBody] postCreateEventDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        dto.description = NormalizeEvent(dto.description);
        dto.homeTeamName = NormalizeEvent(dto.homeTeamName);
        dto.awayTeamName = NormalizeEvent(dto.awayTeamName);
        dto.venueName = NormalizeEvent(dto.venueName);
        dto.venueCity = NormalizeEvent(dto.venueCity);

        if (dto.homeTeamName == dto.awayTeamName)
            return BadRequest("Teams cannot be the same");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var homeTeam = await _context.Teams
                .FirstOrDefaultAsync(t => t.NameOfTeam == dto.homeTeamName);

            if (homeTeam == null)
            {
                homeTeam = new Team
                {
                    NameOfTeam = dto.homeTeamName,
                    TeamInformation = null,
                };

                _context.Teams.Add(homeTeam);
                await _context.SaveChangesAsync();

            }

            var awayTeam = await _context.Teams
                .FirstOrDefaultAsync(t => t.NameOfTeam == dto.awayTeamName);

            if (awayTeam == null)
            {
                awayTeam = new Team
                {
                    NameOfTeam = dto.awayTeamName,
                    TeamInformation = null,
                };

                _context.Teams.Add(awayTeam);
                await _context.SaveChangesAsync();
            }

            var sport = await _context.Sports
                .FirstOrDefaultAsync(s => s.SportName == dto.sportName);

            if (sport == null)
            {
                await transaction.RollbackAsync();
                return BadRequest("Sport not found");
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(v => v.Name == dto.venueName && v.City == dto.venueCity);

            if (venue == null)
            {
                venue = new Venue
                {
                    Name = dto.venueName,
                    City = dto.venueCity
                };

                _context.Venues.Add(venue);
            }

            var newEvent = new Event
            {
                DateTime = DateTime.SpecifyKind(dto.dateTime, DateTimeKind.Utc),
                Description = dto.description,
                _SportId = sport.Id,
                _HomeTeamId = homeTeam.Id,
                _AwayTeamId = awayTeam.Id,
                _VenueId = venue.Id
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await transaction.RollbackAsync();
            return BadRequest("Internal server error");
        }
    }
}
