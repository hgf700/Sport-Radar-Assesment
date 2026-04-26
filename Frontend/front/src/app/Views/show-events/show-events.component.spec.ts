import {  TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { ShowEventsComponent } from './show-events.component';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { sportNameEnum } from '../../enum/sportNameEnum';

describe('ShowEventsComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShowEventsComponent],
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

  it('should display events from API', () => {
    const fixture = TestBed.createComponent(ShowEventsComponent);

    fixture.detectChanges();

    const req = httpMock.expectOne(req =>
      req.method === 'GET' &&
      req.url.includes('/event/show-events')
    );
    
    expect(req.request.method).toBe('GET');

    req.flush([
      {
        id: 1,
        dateTime: '2012-01-01',
        description: 'Football',
        sportName: sportNameEnum.Football,
        homeTeamName: 'Team A',
        awayTeamName: 'Team B',
        venueName: 'venue',
        venueCity: 'city'
      },
      {
        id: 2,
        dateTime: '2012-01-01',
        description: 'Ice_Hockey',
        sportName: sportNameEnum.Ice_Hockey,
        homeTeamName: 'Team A',
        awayTeamName: 'Team B',
        venueName: 'venue',
        venueCity: 'city'
      }
    ]);

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;

    const rows = compiled.querySelectorAll('tbody tr');
    expect(rows.length).toBe(2);
    expect(compiled.textContent).toContain('Ice Hockey');
    expect(compiled.textContent).toContain('Football');
  });
});