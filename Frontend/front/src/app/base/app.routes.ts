import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowEventsComponent } from '../Views/show-events/show-events.component';
import { CreateEventComponent } from '../Views/create-event/create-event.component';

export const routes: Routes = [
    { path: '', component: ShowEventsComponent },
    { path: 'create-event', component: CreateEventComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}