import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './home/feature/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PopupWithInputsComponent } from './shared/ui/popup_with_inputs/popup-with-inputs.component';
import { JwtModule } from '@auth0/angular-jwt';
import { NavigationComponent } from './home/feature/navigation/navigation.component';
import { MatIconModule } from '@angular/material/icon';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ErrorInterceptor } from './interceptor/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { MatSelectModule } from '@angular/material/select';
import { MAT_DATE_LOCALE, MatOptionModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { CalendarComponent } from './home/feature/calendar/calendar.component';
import { CommonModule } from '@angular/common';
import { FlatpickrModule } from 'angularx-flatpickr';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { OrganizationControlComponent } from './org/feature/organization-control/organization-control.component';
import { AdminPanelComponent } from './org/feature/admin-panel/admin-panel.component';
import { RoleRestrictDirective } from './shared/data-access/role-restrict.directive';
import { TimeOffComponent } from './home/feature/time-off/time-off.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SharedTableComponent } from './shared/ui/shared-table/shared-table.component';

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
    TimeOffComponent,
    SharedTableComponent,
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
    MatOptionModule,
    MatDialogModule,
    CommonModule,
    NgbModalModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
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
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
