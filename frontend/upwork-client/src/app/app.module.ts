import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './home/feature/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PopupWithInputsComponent } from './shared/ui/popup-with-inputs/popup-with-inputs.component';
import { JwtModule } from '@auth0/angular-jwt';
import { NavigationComponent } from './home/feature/navigation/navigation.component';
import { MatIconModule } from '@angular/material/icon';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ErrorInterceptor } from './interceptor/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatOptionModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { CalendarComponent } from './org/feature/calendar/calendar.component';
import { CommonModule } from '@angular/common';
import { FlatpickrModule } from 'angularx-flatpickr';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { OrganizationControlComponent } from './org/feature/organization-control/organization-control.component';
import { TimeOffComponent } from './org/feature/time-off/time-off.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { AdminPanelComponent } from './home/feature/admin-panel/admin-panel.component';
import { RoleRestrictDirective } from './shared/data-access/directive/role-restrict.directive';
import { SharedTableComponent } from './shared/ui/shared-table/shared-table.component';
import { PageNotFoundComponent } from './home/feature/page-not-found/page-not-found.component';
import { PermissionsControlComponent } from './org/feature/organization-control/permissions-control/permissions-control.component';
import { RequestTimeOffsComponent } from './org/feature/request-time-offs/request-time-offs.component';
import { AdminPanelPopupComponent } from './home/feature/admin-panel/admin-panel-popup/admin-panel-popup.component';
import { MatCommonModule } from '@angular/material/core';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { DashboardComponent } from './home/feature/dashboard/dashboard.component';
import { AvatarComponent } from './home/feature/avatar/avatar.component';

export function tokenGetter() {
  return localStorage.getItem('access_token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PopupWithInputsComponent,
    NavigationComponent,
    CalendarComponent,
    OrganizationControlComponent,
    AdminPanelComponent,
    RoleRestrictDirective,
    SharedTableComponent,
    PageNotFoundComponent,
    PermissionsControlComponent,
    RequestTimeOffsComponent,
    AdminPanelPopupComponent,
    TimeOffComponent,
    DashboardComponent,
    AvatarComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatIconModule,
    MatSelectModule,
    MatCheckboxModule,
    MatOptionModule,
    MatDialogModule,
    CommonModule,
    NgbModalModule,
    MatDatepickerModule,
    MatCommonModule,
    MatNativeDateModule,
    MatInputModule,
    FlatpickrModule.forRoot(),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    ToastrModule.forRoot({
      timeOut: 3000,
      closeButton: true,
      preventDuplicates: true,
      resetTimeoutOnDuplicate: true,
      progressBar: true,
      progressAnimation: 'decreasing',
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [environment.allowedDomains],
        disallowedRoutes: [],
      },
    }),
  ],
  entryComponents: [PopupWithInputsComponent],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
