import { Component } from '@angular/core';
import { UserService } from 'src/app/shared/data-access/service/user.service';
import { AbsencesService } from 'src/app/shared/data-access/service/absences.service';
import { OrganizationService } from 'src/app/shared/data-access/service/organization.service';
import { RequestsTimeOffsService } from 'src/app/shared/data-access/service/requests-time-offs.service';
import { ToastrService } from 'ngx-toastr';
import { Absence } from 'src/app/models/absence.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  organizationId: string | undefined;
  todayAbsences:Absence[] | undefined;
  weekAbsences:Absence[] | undefined;
  userTodayAbsences:Absence[] | undefined;
  userWeekAbsences:Absence[] | undefined;
  timeOffRequests: Absence[] | undefined;
  public showName = false;
  public url = '';
  constructor(private userService: UserService, 
    private absencesService: AbsencesService, 
    private tostr: ToastrService, 
    private router: Router,
    private organizationService: OrganizationService,
    private requestsTimeOffsService: RequestsTimeOffsService) {
    this.userService.user$.subscribe((user) => {
      if (user) {
        this.organizationId = user.organizationId;
      }
    });
    
    const options:Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    const todayDate = new Date().toLocaleString('en-CA', options);
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    const tomorrowDate=tomorrow.toLocaleString('en-CA', options);
    const week=new Date();
    week.setDate(week.getDate()+7);
    const weekDate=week.toLocaleString('en-CA', options);
    this.requestsTimeOffsService.getListOfRequests('a',0,10).subscribe((res: { data: Absence[] | undefined; })=>{
      if (res) {
        this.timeOffRequests=res.data;
      }
    });
    this.absencesService.getAbsencesByOrganizationId(this.organizationId,tomorrowDate,weekDate,0,10).subscribe((res)=>{
      if (res) {
        this.weekAbsences=res.data;
      }
    });
    this.absencesService.getAbsencesByOrganizationId(this.organizationId,todayDate,tomorrowDate,0,10).subscribe((res)=>{
      if (res) {
        this.todayAbsences=res.data;
      }
    });
    this.absencesService.getAbsencesForUser(todayDate,tomorrowDate,0,10).subscribe((res)=>{
      if (res) {
        this.userTodayAbsences=res.data;
      }
    });
    this.absencesService.getAbsencesForUser(tomorrowDate,weekDate,0,10).subscribe((res)=>{
      if (res) {
        this.userWeekAbsences=res.data;
      }
    });
    
   }
  navigateToCalendar() {
    this.router.navigate([`/org/${this.url}/calendar`])
    .then(() => {
      // Navigation successful
    })
    .catch((error) => {
      // Handle the error if needed
    });
  }
  navigateToRequests() {
    this.router.navigate([`/org/${this.url}/requests`])
    .then(() => {
      
    })
    .catch((error) => {
      // Handle the error if needed
    });
  }
  navigateToTimeOff() {
    //this.router.navigate([``]);
  }

  ngOnInit(): void {
    this.organizationService.organization$.subscribe(res => {
      if (res?.urlName) {
        this.url = res?.urlName;
        
      }
    });
  }
}
