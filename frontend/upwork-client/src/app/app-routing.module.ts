import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './home/feature/login/login.component';
import { OrganizationControlComponent } from './org/feature/organization-control/organization-control.component';
import { CalendarComponent } from './home/feature/calendar/calendar.component';
import { AdminPanelComponent } from './org/feature/admin-panel/admin-panel.component';
import { authGuard } from './shared/auth/auth.guard';
import { adminGuard } from './shared/data-access/admin.guard';
import { TimeOffComponent } from './home/feature/time-off/time-off.component';

const routes: Routes = [
  { path: 'time-off', component: TimeOffComponent },
  { path: 'login', component: LoginComponent },
  {
    path: '',
    canActivate: [authGuard],
    children: [
      {
        path: 'admin-panel',
        component: AdminPanelComponent,
        canActivate: [adminGuard],
      },
      { path: 'calendar', component: CalendarComponent },
      { path: 'organization-control', component: OrganizationControlComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
