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
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { Absence } from 'src/app/models/absence.model';
import { AbsenceType } from 'src/app/models/enums/absence-type.enum';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup-with-inputs/popup-with-inputs.component';
import { formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { TimeConverter } from 'src/app/shared/web-utilities/time-converter';
import { ApprovalState } from 'src/app/models/enums/approval-state.enum';
import { AbsenceService } from 'src/app/shared/data-access/service/absence.service';

@Component({
  selector: 'app-time-off',
  templateUrl: './time-off.component.html',
  styleUrls: ['./time-off.component.scss'],
})
export class TimeOffComponent {
  header = ['From date', 'To date', 'Type', 'Status'];
  currentPage$ = new BehaviorSubject<number>(0);
  listOfUserRequests$: Observable<SharedTableData[]> = this.loadUserRequests();
  totalNumberOfPages = 1;
  absencesYearCount$ = this.getAbsencesYearCount();

  constructor(
    private dialog: MatDialog,
    private absenceService: AbsenceService,
    private tostr: ToastrService
  ) {}

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
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  cancelRequest(requestId: string): void {
    this.absenceService.cancelRequest(requestId).subscribe(isCancelled => {
      if (isCancelled) {
        this.tostr.success('Time Off request cancelled successfully');
      } else {
        this.tostr.warning('Something went wrong');
      }
      this.listOfUserRequests$ = this.loadUserRequests();
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
        selectOptions: Object.keys(AbsenceType)
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
    const userRequest: Absence = {
      fromDate: TimeConverter.createDateAsUTC(
        new Date(String(inputs['TimeOffBeginningDate'].value))
      ),
      toDate: TimeConverter.createDateAsUTC(
        new Date(String(inputs['TimeOffEndDate'].value))
      ),
      absenceType:
        AbsenceType[inputs['TimeOffOptions'].value as keyof typeof AbsenceType],
    };

    this.absenceService
      .createAbsenceRequest(userRequest)
      .subscribe(isSuccess => {
        if (isSuccess) {
          this.tostr.success('New Time Off request created successfully');
        } else {
          this.tostr.success('Something went wrong');
        }
        this.listOfUserRequests$ = this.loadUserRequests();
      });
  }

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }

  private getAbsencesYearCount(): Observable<number> {
    return this.absenceService.getYearAbsenceCountForUser();
  }

  private loadUserRequests(): Observable<SharedTableData[]> {
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
          formatDate(userRequest.fromDate, 'dd/MM/yyyy', 'en-US'),
          formatDate(userRequest.toDate, 'dd/MM/yyyy', 'en-US'),
          AbsenceType[Number(userRequest.absenceType.toString())].replace(
            /([A-Z])/g,
            ' $1'
          ),
        ],
        actions: [],
      };

      if (new Date(userRequest.fromDate) > new Date()) {
        result.actions?.push({
          icon: 'delete',
          func: (arg: string) => {
            this.openCancelRequestPopup(arg);
          },
          arg: userRequest.id,
        });
      } else {
        result.cols?.push(
          ApprovalState[Number(userRequest.approvalState?.toString())]
        );
      }
      results.push(result);
    });
    return results;
  }
}
