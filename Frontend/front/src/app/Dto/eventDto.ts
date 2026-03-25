import { sportNameEnum } from '../enum/sportNameEnum';

export interface eventDto {
  id?: number;
  dateTime: Date;
  description: string;
  sportName: sportNameEnum;
  homeTeamName: string;
  awayTeamName: string;
  venueName: string;
  venueCity: string;
}
