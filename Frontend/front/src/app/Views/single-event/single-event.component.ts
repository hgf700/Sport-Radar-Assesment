import { Component, OnInit } from '@angular/core';
import { EventService } from '../../Services/EventService';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { eventDto } from '../../Dto/eventDto';
import { sportNameEnum } from '../../enum/sportNameEnum';

@Component({
  selector: 'app-single-event',
  imports: [CommonModule, RouterModule],
  standalone: true,
  templateUrl: './single-event.component.html',
  styleUrl: './single-event.component.css'
})
export class SingleEventComponent implements OnInit{
  event: eventDto[] = [];
  eventId!: string;

  constructor(
    private eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.eventId = params.get('id')!;
      this.loadSingleEvent();
    });
  }

  sportMap: Record<number, string> = {
    [sportNameEnum.Football]: 'piłka nożna',
    [sportNameEnum.Ice_Hockey]: 'hokej na lodzie'
  };

  loadSingleEvent() {
    this.eventService.getSingleEvent(this.eventId).subscribe({
      next: (data) => {
        this.event = data;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  showEventsView() {
    this.router.navigate(['/']);
  }
}
