import { Component, OnInit } from '@angular/core';
import { EventService } from '../../Services/EventService';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { eventDto } from '../../Dto/eventDto';
import { sportNameEnum } from '../../enum/sportNameEnum';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-show-events',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  standalone: true,
  templateUrl: './show-events.component.html',
  styleUrl: './show-events.component.css',
})
export class ShowEventsComponent implements OnInit {
  event: eventDto[] = [];
  selectSingleEventForm!: FormGroup;
  submitted = false;

  constructor(
    private eventService: EventService,
    private router: Router,
    private fb: FormBuilder,
  ) {
    this.selectSingleEventForm = this.fb.group({
      eventId: [''],
    });
  }

  ngOnInit(): void {
    this.loadEvents();
  }

  sportMap: Record<number, string> = {
    [sportNameEnum.Football]: 'Football',
    [sportNameEnum.Ice_Hockey]: 'Ice Hockey',
  };

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

  get f() {
    return this.selectSingleEventForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.selectSingleEventForm.invalid) return;

    const id = this.selectSingleEventForm.value.eventId;

    this.router.navigate(['/single-event', id]);
  }
}
