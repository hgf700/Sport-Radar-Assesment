import { Component, OnInit } from '@angular/core';
import { EventService } from '../../Services/EventService';
import { eventDto } from '../../Dto/eventDto';

@Component({
  selector: 'app-show-events',
  imports: [],
  standalone: true,
  templateUrl: './show-events.component.html',
  styleUrl: './show-events.component.css'
})
export class ShowEventsComponent implements OnInit{
  event: eventDto[] = [];

  constructor(
    private eventService: EventService,
  ) {}

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents() {
    this.eventService.getEvents().subscribe({
      next: (data) => {
        this.event = data;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
