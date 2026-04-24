import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { EventService } from '../../Services/EventService';
import { eventDto } from '../../Dto/eventDto';
import { sportNameEnum } from '../../enum/sportNameEnum';

@Component({
  selector: 'app-create-event',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './create-event.component.html',
  styleUrl: './create-event.component.css',
})
export class CreateEventComponent {
  event: eventDto[] = [];
  createEventForm!: FormGroup;
  submitted = false;

  isLoading = false;
  errorMessage: string | null = null;

  constructor(
    private eventService: EventService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.createEventForm = this.fb.group({
      dateTime: ['', 
        [Validators.required]],
      description: [''],
      sportName: [sportNameEnum.Football, 
        [Validators.required]],
      homeTeamName: ['', 
        [Validators.required, Validators.minLength(1)]],
      awayTeamName: ['',
        [Validators.required, Validators.minLength(1)]],
      venueName: ['', 
        [Validators.required,Validators.minLength(1)]],
      venueCity: ['', 
        [Validators.required,Validators.minLength(1)]],
    });
  }

  sportOptions = [
    { value: sportNameEnum.Football, label: 'Football' },
    { value: sportNameEnum.Ice_Hockey, label: 'Ice Hockey' },
  ];

  showEventsView() {
    this.router.navigate(['/']);
  }

  // getter ułatwiający dostęp do pól w HTML
  get f() {
    return this.createEventForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    this.errorMessage = null;

    if (this.createEventForm.invalid) return;
    
    this.isLoading = true;

    this.eventService.createEvent(this.createEventForm.value).subscribe({
      next: () => {
        this.isLoading = false;
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage =
          err?.error || 'Wystąpił błąd podczas tworzenia wydarzenia';
      },
    });
  }
}