import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { sportNameEnum } from '../../enum/sportNameEnum';
import { SingleEventComponent } from './single-event.component';
import { ActivatedRoute } from '@angular/router';

describe('SingleEventComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SingleEventComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: (key: string) => '1'
              }
            }
          }
        }
      ]
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should display single event from API', () => {
    const fixture = TestBed.createComponent(SingleEventComponent);

    fixture.detectChanges();

    const req = httpMock.expectOne(req =>
      req.method === 'GET' &&
      req.url.includes('/event/show-selected-event/1')
    );

    expect(req.request.method).toBe('GET');

    req.flush({
      id: 1,
      dateTime: '2012-01-01',
      description: 'Football',
      sportName: sportNameEnum.Football,
      homeTeamName: 'Team A',
      awayTeamName: 'Team B',
      venueName: 'venue',
      venueCity: 'city'
    });

    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;

    const rows = compiled.querySelectorAll('tbody tr');
    expect(rows.length).toBe(1);

    expect(compiled.textContent).toContain('Football');
  });
});