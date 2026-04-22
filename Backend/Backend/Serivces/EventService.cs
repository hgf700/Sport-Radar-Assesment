using Backend.Models;

namespace Backend.Serivces;

public class EventService
{
    private readonly IRepositoryStrategy<Event> _eventRepository;

    public EventService(IRepositoryStrategy<Event> eventRepository
        )
    {
        _eventRepository= eventRepository;
    }

    //public async Task<(bool Success, string? Error)> CreateEvent(Event event)
    //{

    //    await _eventRepository.Add(event);

    //    return (true, null);
    //}
}
