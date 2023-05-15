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
import { AbsenceService } from '../../../shared/absence/absence.service';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { Absence } from 'src/app/models/absence.model';
import { SharedTableComponent } from '../../../shared/ui/shared-table/shared-table.component';
import { AbsenceTypes } from 'src/app/models/enums/absence-types.enum';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup-with-inputs/popup-with-inputs.component';
import { formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-time-off',
  templateUrl: './time-off.component.html',
  styleUrls: ['./time-off.component.scss'],
})
export class TimeOffComponent {
  //time_off_days = 21; //TODO: to be replaced with value calculated from db
  header = ['Time offs taken', 'Statuses'];
  currentPage$ = new BehaviorSubject<number>(0);
  listOfUserRequests$: Observable<SharedTableData[]> = this.loadUserRequests();
  totalNumberOfPages = 1;
  absencesYearCount$ = this.getAbsencesYearCount();
  //listOfUsers$: Observable<PaginatedResult<UserWithPermissions>>

  constructor(
    private dialog: MatDialog,
    private absenceService: AbsenceService,
    private tostr: ToastrService
  ) {}

  private getAbsencesYearCount(): Observable<number> {
    return this.absenceService.getYearAbsenceCountForUser();
  }

  loadUserRequests(): Observable<SharedTableData[]> {
    return this.currentPage$.pipe(
      switchMap(currentPage =>
        this.absenceService.getAbsencesForUser(currentPage)
      ),
      map((res: PaginatedResult<Absence>) => {
        this.totalNumberOfPages = res.page ?? 1;
        if (res.data.length === 0 && this.currentPage$.value - 1 >= 0)
          this.currentPage$.next(this.currentPage$.value - 1);
        return this.mapData(res);
      })
    );
  }

  private mapData(data: PaginatedResult<Absence>): SharedTableData[] {
    const userRequests = data.data;
    const results: SharedTableData[] = [];
    userRequests.forEach(userRequest => {
      const result: SharedTableData = {
        cols: [
          formatDate(userRequest.fromDate, 'dd-MM-yyyy', 'en-US') +
            ' - ' +
            formatDate(userRequest.toDate, 'dd-MM-yyyy', 'en-US'),
        ],
        actions: [
          {
            icon: 'delete',
            func: (arg: string) => {
              this.openCancelRequestPopup(arg);
            },
            arg: userRequest.id,
          },
        ],
      };
      results.push(result);
    });
    return results;
  }

  openCancelRequestPopup(requestId: string): void {
    const inputs: Dictionary<InputPopupModel> = {};
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Yes',
        onClick: () => this.cancelRequest(requestId),
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'NO',
      },
    ];

    const data: InputPopupDataModel = {
      title: 'Cancel organization',
      description: 'Are you sure you want to cancel your request?',
      inputs: inputs,
      buttons: buttons,
    };
    console.log('kot');
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  cancelRequest(requestId: string): void {
    this.absenceService.cancelRequest(requestId).subscribe(isCancelled => {
      if (!isCancelled) this.tostr.warning('Something went wrong');
      else this.listOfUserRequests$ = this.loadUserRequests();
    });
  }

  openNewRequestPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
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
        //TODO: to be replaced with values from API - specific absence types for organization
        selectOptions: Object.keys(AbsenceTypes)
          .filter(key => isNaN(Number(key)))
          .map(key => ({
            value: key,
            displayValue: key.replace(/([A-Z])/g, ' $1').trim(),
          })),
      },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Submit',
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

    const userRequest: Absence = {
      fromDate: new Date(String(inputs['TimeOffBeginningDate'].value)),
      toDate: new Date(String(inputs['TimeOffEndDate'].value)),
      absenceType:
        AbsenceTypes[
          inputs['TimeOffOptions'].value as keyof typeof AbsenceTypes
        ],
    };

    this.absenceService.createAbsenceRequest(userRequest).subscribe(() => {
      this.listOfUserRequests$ = this.loadUserRequests();
    });
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
