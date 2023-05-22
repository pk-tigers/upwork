import { Component } from '@angular/core';
import { AbsencesService } from 'src/app/shared/data-access/service/absences.service';
import { OrganizationService } from 'src/app/shared/data-access/service/organization.service';
import { RequestsTimeOffsService } from 'src/app/shared/data-access/service/requests-time-offs.service';
import { ToastrService } from 'ngx-toastr';
import { Absence } from 'src/app/models/absence.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  organizationId: string | undefined;
  todayAbsences: Absence[] | undefined;
  weekAbsences: Absence[] | undefined;
  userTodayAbsences: Absence[] | undefined;
  userWeekAbsences: Absence[] | undefined;
  timeOffRequests: Absence[] | undefined;
  public showName = false;
  public url = '';
  containers: {
    type: string;
    header: string;
    items: { today: Absence[]; week: Absence[] } | undefined;
    buttonAction: () => Promise<void>;
  }[] = [];
  constructor(
    private absencesService: AbsencesService,
    private tostr: ToastrService,
    private router: Router,
    private organizationService: OrganizationService,
    private requestsTimeOffsService: RequestsTimeOffsService
  ) {
    this.organizationService.organization$.subscribe(res => {
      if (res) {
        this.organizationId = res.id;
        this.url = res?.urlName;
        this.fetchData();
      }
    });
  }

  fetchData(): void {
    const todayDate = new Date();
    const tomorrowDate = new Date();
    tomorrowDate.setDate(tomorrowDate.getDate() + 1);
    const weekDate = new Date();
    weekDate.setDate(weekDate.getDate() + 7);

    this.requestsTimeOffsService
      .getListOfRequests(0, 10)
      .subscribe((res: { data: Absence[] | undefined }) => {
        if (res) {
          this.timeOffRequests = res.data;
          this.updateContainers();
        }
      });

    this.absencesService
      .getAbsencesByOrganizationId(
        this.organizationId,
        todayDate,
        weekDate,
        0,
        10
      )
      .subscribe(res => {
        if (res) {
          const { today, week } = this.categorizeAbsencesByDate(
            res.data,
            todayDate,
            weekDate
          );
          this.todayAbsences = today;
          this.weekAbsences = week;
          this.updateContainers();
        }
      });

    this.absencesService
      .getAbsencesForUser(tomorrowDate, weekDate, 0, 10)
      .subscribe(res => {
        if (res) {
          const { today, week } = this.categorizeAbsencesByDate(
            res.data,
            todayDate,
            weekDate
          );
          this.userTodayAbsences = today;
          this.userWeekAbsences = week;
          this.updateContainers();
        }
      });
  }

  updateContainers(): void {
    if (
      this.todayAbsences &&
      this.weekAbsences &&
      this.userTodayAbsences &&
      this.userWeekAbsences &&
      this.timeOffRequests
    ) {
      this.containers = [
        {
          type: 'time',
          header: 'Upcoming time off',
          items: { today: this.todayAbsences, week: this.weekAbsences },
          buttonAction: async () => {
            await this.router.navigate([`/org/${this.url}/calendar`]);
          },
        },
        {
          type: 'time',
          header: 'Your time off',
          items: { today: this.userTodayAbsences, week: this.userWeekAbsences },
          buttonAction: async () => {
            await this.router.navigate([`/org/${this.url}/time-off`]);
          },
        },
        {
          type: 'requests',
          header: 'Employee’s time off requests',
          items: { today: this.timeOffRequests, week: [] },
          buttonAction: async () => {
            await this.router.navigate([`/org/${this.url}/requests`]);
          },
        },
      ];
    }
  }

  categorizeAbsencesByDate(
    absences: Absence[],
    tomorrowDate: Date,
    weekDate: Date
  ): { today: Absence[]; week: Absence[] } {
    const today: Absence[] = [];
    const week: Absence[] = [];

    absences.forEach(absence => {
      if (new Date(absence.fromDate) === tomorrowDate) {
        today.push(absence);
      } else if (new Date(absence.fromDate) <= weekDate) {
        week.push(absence);
      }
    });
    return { today, week };
  }
}
