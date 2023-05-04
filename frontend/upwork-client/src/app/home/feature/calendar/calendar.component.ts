/* eslint-disable @typescript-eslint/no-inferrable-types */
import '../../../../_variables.scss';
import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
} from '@angular/core';
import {
  startOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
} from 'date-fns';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { EventColor } from 'calendar-utils';

const colors: Record<string, EventColor> = {
  blue: {
    primary: '#92a7ff',
    secondary: '#d6dcff',
  },
  orange: {
    primary: '#f8c48c',
    secondary: '#fcebd9',
  },
  beige: {
    primary: '#a39999',
    secondary: '#dedcdc',
  },
};

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

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  modalData!: {
    action: string;
    event: CalendarEvent;
  };

  events: CalendarEvent[] = [
    {
      start: subDays(startOfDay(new Date()), 1),
      end: addDays(new Date(), 2),
      title: 'A 4 day blue event',
      color: { ...colors['blue'] },
    },
    {
      start: subDays(endOfMonth(new Date()), 3),
      end: addDays(endOfMonth(new Date()), 10),
      title: 'A long event that spans 2 months and is beige',
      color: { ...colors['beige'] },
      allDay: true,
    },
    {
      start: startOfDay(new Date()),
      title: 'A orange event',
      color: { ...colors['orange'] },
    },
  ];

  activeDayIsOpen: boolean = false;

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
