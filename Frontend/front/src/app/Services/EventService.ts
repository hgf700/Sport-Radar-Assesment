import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { eventDto } from '../Dto/eventDto';

@Injectable({ providedIn: 'root' })
export class EventService {
  private apiUrl = 'https://localhost:7170/event';

  constructor(private http: HttpClient) {}

  event: eventDto[] = [];

  getEvents() {
    return this.http.get<eventDto[]>(`${this.apiUrl}/show-events`, {});
  }

  getSingleEvent(eventId: number) {
    return this.http.get<eventDto[]>(`${this.apiUrl}/show-selected-event${eventId}`, {});
  }

  createEvent(dto: eventDto) {
    return this.http.post(`${this.apiUrl}/create-new-event`, { dto });
  }
}
