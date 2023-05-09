import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Dictionary } from 'cypress/types/lodash';
import { Observable, BehaviorSubject, switchMap, map } from 'rxjs';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup_with_inputs/popup-with-inputs.component';
import { AbsenceService } from '../../../shared/absence/absence.service';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { AbsenceModel } from 'src/app/models/absence.model';
import { SharedTableComponent } from '../../../shared/ui/shared-table/shared-table.component';

@Component({
  selector: 'app-time-off',
  templateUrl: './time-off.component.html',
  styleUrls: ['./time-off.component.scss'],
})
export class TimeOffComponent {
  time_off_days = 21; //TODO: to be replaced with value calculated from db
  header = ['Time offs taken', 'Statuses'];
  currentPage$ = new BehaviorSubject<number>(0);
  listOfUserRequests$: Observable<SharedTableData[]> = this.loadUserRequests();
  totalNumberOfPages = 1;

  constructor(
    private dialog: MatDialog,
    private absenceService: AbsenceService
  ) {}

  loadUserRequests(): Observable<SharedTableData[]> {
    const guid = '68796C52-594E-47D0-FDAB-08DB4D65E637';
    const from = new Date(2022, 10, 10);
    const to = new Date(2023, 5, 10);
    console.log('1');
    return this.currentPage$.pipe(
      switchMap(currentPage =>
        this.absenceService.getAbsencesForUser(guid, from, to, currentPage)
      ),
      map((res: PaginatedResult<AbsenceModel>) => {
        this.totalNumberOfPages = res.page ?? 1;
        if (res.data.length === 0 && this.currentPage$.value - 1 >= 0)
          this.currentPage$.next(this.currentPage$.value - 1);
        return this.mapData(res);
      })
    );
  }

  private mapData(data: PaginatedResult<AbsenceModel>): SharedTableData[] {
    const userRequests = data.data;
    const results: SharedTableData[] = [];
    userRequests.forEach(userRequest => {
      const result: SharedTableData = {
        cols: [userRequest.fromDate.toISOString()],
        actions: [
          //TODO
        ],
      };
      results.push(result);
    });
    return results;
  }

  openNewRequestPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['TimeOffNote']: {
        value: '',
        type: 'text',
        placeholder: 'Add a note',
      },
      ['TimeOffBeginningDate']: {
        value: '',
        type: 'date',
        placeholder: 'Enter beginning date',
      },
      ['TimeOffEndDate']: {
        value: '',
        type: 'date',
        placeholder: 'Enter end date',
      },
      ['TimeOffOptions']: {
        value: '',
        type: 'select',
        placeholder: 'Select type of Time off',
        selectOptions: [
          { value: 'paid-time-off', displayValue: 'Paid time off' },
          { value: 'sick-leave', displayValue: 'Sick leave' },
          { value: 'unpaid-leave', displayValue: 'Unpaid leave' },
        ],
      },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Submit',
        // onClick: () => this.createTimeOffRequest(inputs),
        onClick: () => this.createTimeOffRequest(inputs),
      },
    ];
    const data: InputPopupDataModel = {
      title: 'New time off request',
      description: 'Fill basic data about your request:',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  createTimeOffRequest(inputs: Dictionary<InputPopupModel>): void {
    //TODO: add logic about creating new request by user
    for (const key in inputs) {
      console.log(inputs[key]);
    }
  }

  goTo(urlName: string | undefined): void {
    if (typeof urlName === 'undefined') return;
    // TODO: route to correct url
    // this.router.navigate([`/${urlName}/dashboard`]);
  }

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }
}
