import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './home/feature/login/login.component';
import { OrganizationControlComponent } from './org/feature/organization-control/organization-control.component';
import { CalendarComponent } from './home/feature/calendar/calendar.component';
import { AdminPanelComponent } from './org/feature/admin-panel/admin-panel.component';
import { authGuard } from './shared/auth/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    canActivate: [authGuard],
    children: [
      { path: 'admin-panel', component: AdminPanelComponent },
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
