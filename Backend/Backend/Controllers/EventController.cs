using Backend.DB;
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
    public async Task<IActionResult> ShowEvents()
    {
        var events = await _context.Events.ToListAsync();

        return Ok(events);
    }

    [HttpGet("show-selected-event/{eventId}")]
    public async Task<IActionResult> ShowSelectedEvent(int eventId)
    {

        var selectedEvent= await _context.Events.FirstOrDefaultAsync(e => e.Id== eventId);

        return Ok(selectedEvent);
    }

    [HttpPost("create-new-event")]
    public async Task<IActionResult> CreateNewEvent()
    {

        return Ok();
    }
    
}
