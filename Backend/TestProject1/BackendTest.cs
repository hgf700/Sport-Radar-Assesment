using Backend.DB;
using Backend.Models;
using Backend.Models.Dto;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace TestProject1;

public class EventApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public EventApiTests(WebApplicationFactory<Program> factory
        )
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task POST_event_and_get_exact_id_test_pipe_200()
    {
        var dto = new postCreateEventDto
        {
            dateTime = DateTime.UtcNow,
            description = "         Test    event   ",
            sportName = SportName.Ice_Hockey,
            homeTeamName = "A",
            awayTeamName = "B",
            venueName = "Stadium",
            venueCity = "City"
        };

        var createResponse = await _client.PostAsJsonAsync("/event/create-new-event", dto);

        createResponse.EnsureSuccessStatusCode();

        var raw = await createResponse.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrEmpty(raw));

        var created = JsonSerializer.Deserialize<getSelectedEventDto>(raw,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(created);
        Assert.True(created.id > 0);
    }

    [Fact]
    public async Task GET_events_200()
    {
        var response = await _client.GetAsync("/event/show-events");
        var text = await response.Content.ReadAsStringAsync();

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(text);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task POST_invalid_event_500()
    {
        var dto = new postCreateEventDto
        {
            dateTime = DateTime.UtcNow,
            description = "x",
            sportName = SportName.Ice_Hockey,
            homeTeamName = "", // ❌ invalid
            awayTeamName = "B",
            venueName = "Stadium",
            venueCity = "City"
        };

        var response = await _client.PostAsJsonAsync("/event/create-new-event", dto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GET_non_existing_event_404()
    {
        //because ef 
        var response = await _client.GetAsync("/event/show-selected-event/999999");

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task POST_same_teams_500()
    {
        var dto = new postCreateEventDto
        {
            dateTime = DateTime.UtcNow,
            description = "x",
            sportName = SportName.Ice_Hockey,
            homeTeamName = "B", // ❌ invalid
            awayTeamName = "B",
            venueName = "Stadium",
            venueCity = "City"
        };

        var response = await _client.PostAsJsonAsync("/event/create-new-event", dto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task POST_sportname_null_500()
    {
        var dto = new postCreateEventDto
        {
            dateTime = DateTime.UtcNow,
            description = "x",
            homeTeamName = "B", 
            awayTeamName = "B",
            venueName = "Stadium",
            venueCity = "City"
        };

        var response = await _client.PostAsJsonAsync("/event/create-new-event", dto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task POST_whitespaces_500()
    {
        var dto = new postCreateEventDto
        {
            dateTime = DateTime.UtcNow,
            sportName = SportName.Ice_Hockey,
            description = "    ",
            homeTeamName = "    ",
            awayTeamName = "   ",
            venueName = "    ",
            venueCity = "       "
        };

        var response = await _client.PostAsJsonAsync("/event/create-new-event", dto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
