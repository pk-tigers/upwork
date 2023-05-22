import '../../../../_variables.scss';
import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
} from '@angular/core';
import { isSameDay, isSameMonth } from 'date-fns';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { EventColor } from 'calendar-utils';
import { AbsenceService } from 'src/app/shared/data-access/service/absence.service';
import { OrganizationService } from 'src/app/shared/data-access/service/organization.service';
import { Observable, map, switchMap } from 'rxjs';
import { TimeUtilities } from 'src/app/shared/web-utilities/time-utilities';
import { UserAbsence } from 'src/app/models/user-absence.model';
import { AbsenceType } from 'src/app/models/enums/absence-type.enum';
import { colors } from 'src/app/models/colors/color';

@Component({
  selector: 'app-calendar',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss'],
})
export class CalendarComponent {
  @ViewChild('modalContent', { static: true })
  modalContent!: TemplateRef<unknown>;
  view: CalendarView = CalendarView.Month;
  activeDayIsOpen = false;
  calendarView = CalendarView;
  viewDate: Date = new Date();
  from: Date = TimeUtilities.getFirstDayOfMonth(new Date());
  to: Date = TimeUtilities.getLastDayOfMonth(new Date());
  events: Observable<CalendarEvent<string>[]> = this.loadAbsences();
  diffInMonths = 0;

  constructor(
    private absenceService: AbsenceService,
    private organizationService: OrganizationService
  ) {}

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (!isSameMonth(date, this.viewDate)) return;
    if (
      (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
      events.length === 0
    )
      this.activeDayIsOpen = false;
    else this.activeDayIsOpen = true;

    this.viewDate = date;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }

  presentMonth() {
    this.diffInMonths = 0;
    this.from = TimeUtilities.getFirstDayOfMonth(new Date());
    this.to = TimeUtilities.getLastDayOfMonth(new Date());
    this.events = this.loadAbsences();
  }

  nextMonth() {
    this.diffInMonths--;
    this.from = TimeUtilities.getFirstDayOfMonth(
      TimeUtilities.getDateDifferentByMonths(this.diffInMonths)
    );
    this.to = TimeUtilities.getLastDayOfMonth(
      TimeUtilities.getDateDifferentByMonths(this.diffInMonths)
    );
    this.events = this.loadAbsences();
  }

  prevMonth() {
    this.diffInMonths++;
    this.from = TimeUtilities.getFirstDayOfMonth(
      TimeUtilities.getDateDifferentByMonths(this.diffInMonths)
    );
    this.to = TimeUtilities.getLastDayOfMonth(
      TimeUtilities.getDateDifferentByMonths(this.diffInMonths)
    );
    this.events = this.loadAbsences();
  }

  private loadAbsences(): Observable<CalendarEvent[]> {
    return this.organizationService.organization$.pipe(
      switchMap(organization =>
        this.absenceService.getAbsencesMonthly(
          organization?.id,
          this.from,
          this.to
        )
      ),
      map(res => this.mapData(res))
    );
  }

  private mapData(data: UserAbsence[]): CalendarEvent[] {
    const events: CalendarEvent[] = [];
    data.forEach(absence => {
      const event: CalendarEvent = {
        start: new Date(absence.fromDate),
        end: new Date(absence.toDate),
        title: `${absence.firstName} ${
          absence.lastName
        } - ${this.getAbsenceTypeTitle(absence.absenceType)}`,
        color: this.getAbsenceTypeColor(absence.absenceType),
        meta:
          absence.firstName[0].toUpperCase() +
          absence.lastName[0].toUpperCase(),
      };
      events.push(event);
    });
    return events;
  }

  private getAbsenceTypeTitle(absenceType: AbsenceType): string {
    const absenceTypeStrings = [
      'Paid time off',
      'Unpaid leave',
      'Sick leave',
      'Maternity leave',
    ];
    return absenceTypeStrings[absenceType];
  }

  private getAbsenceTypeColor(absenceType: AbsenceType): EventColor {
    const absenceTypeStrings = ['orange', 'blue', 'beige', 'beige'];
    return { ...colors[absenceTypeStrings[absenceType]] };
  }
}
