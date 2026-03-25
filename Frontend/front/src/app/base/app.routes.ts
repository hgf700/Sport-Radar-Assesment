import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowEventsComponent } from '../Views/show-events/show-events.component';
import { CreateEventComponent } from '../Views/create-event/create-event.component';
import { SingleEventComponent } from '../Views/single-event/single-event.component';

export const routes: Routes = [
  { path: '', component: ShowEventsComponent },
  { path: 'create-event', component: CreateEventComponent },
  { path: 'single-event/:id', component: SingleEventComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
