import { Component, OnInit } from '@angular/core';
import { EventService } from '../../Services/EventService';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { eventDto } from '../../Dto/eventDto';

@Component({
  selector: 'app-show-events',
  imports: [CommonModule, RouterModule],
  standalone: true,
  templateUrl: './show-events.component.html',
  styleUrl: './show-events.component.css'
})
export class ShowEventsComponent implements OnInit{
  event: eventDto[] = [];

  constructor(
    private eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
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

  createEventView() {
    this.router.navigate(['/create-event']);
  }
}
