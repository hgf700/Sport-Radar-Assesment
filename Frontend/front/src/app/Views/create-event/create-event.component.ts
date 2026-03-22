import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { EventService } from '../../Services/EventService';
import { eventDto } from '../../Dto/eventDto';

@Component({
  selector: 'app-create-event',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './create-event.component.html',
  styleUrl: './create-event.component.css'
})

export class CreateEventComponent {
  event: eventDto[] = [];
  createEventForm!: FormGroup;
  submitted = false;

  constructor(
    private eventService: EventService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.createEventForm = this.fb.group({
      dateTime: [''],
      description: [''],
      sportName: [''],
      homeTeamName: [''],
      awayTeamName: [''],
      venueName: [''],
      venueCity: ['']
    });
  }

  showEventsView() {
    this.router.navigate(['/']);
  }

  onSubmit() {
    this.submitted = true;

    if (this.createEventForm.invalid) return;

    this.eventService.createEvent(this.createEventForm.value).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (err) => alert(err.error),
    });
  }

}
