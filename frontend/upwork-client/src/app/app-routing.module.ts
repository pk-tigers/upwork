import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './home/feature/login/login.component';
import { OrganizationControlComponent } from './org/feature/organization-control/organization-control.component';
import { CalendarComponent } from './org/feature/calendar/calendar.component';
import { AdminPanelComponent } from './home/feature/admin-panel/admin-panel.component';
import { authGuard } from './shared/auth/auth.guard';
import { TimeOffComponent } from './org/feature/time-off/time-off.component';
import { adminGuard } from './shared/data-access/guard/admin.guard';
import { PageNotFoundComponent } from './home/feature/page-not-found/page-not-found.component';
import { organizationGuard } from './shared/data-access/guard/organization.guard';
import { RequestTimeOffsComponent } from './org/feature/request-time-offs/request-time-offs.component';
import { ProfileComponent } from './home/feature/profile/profile.component';

import { DashboardComponent } from './home/feature/dashboard/dashboard.component';
const routes: Routes = [
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
      {
        path:'profile',
        component: ProfileComponent
      },
      {
        path: 'org/:org-url',
        canActivate: [organizationGuard],
        children: [
          { path: 'time-off', component: TimeOffComponent },
          { path: 'calendar', component: CalendarComponent },
          {
            path: 'organization-control',
            component: OrganizationControlComponent,
          },
          { path: 'requests', component: RequestTimeOffsComponent },
          { path: 'dashboard', component: DashboardComponent },
        ],
      },
    ],
  },

  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
