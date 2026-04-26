import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { CreateEventComponent } from './create-event.component';
import { sportNameEnum } from '../../enum/sportNameEnum';

describe('CreateEventComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateEventComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create event (POST request)', () => {
    const fixture = TestBed.createComponent(CreateEventComponent);
    const component = fixture.componentInstance;

    fixture.detectChanges(); // 🔥 inicjalizacja form

    component.createEventForm.setValue({
      description: 'Test match',
      homeTeamName: 'Team A',
      awayTeamName: 'Team B',
      sportName: sportNameEnum.Football,
      venueName: 'venue',
      venueCity: 'city',
      dateTime: '2020-01-01'
    });

    component.onSubmit();

    const req = httpMock.expectOne(req =>
      req.method === 'POST' &&
      req.url.includes('/event/create-new-event')
    );

    expect(req.request.method).toBe('POST');

    req.flush({
      id: 1,
      message: 'created'
    });
  });
});