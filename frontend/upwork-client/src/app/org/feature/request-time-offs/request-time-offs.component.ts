import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Observable, map, switchMap } from 'rxjs';
import { Absence } from 'src/app/models/absence.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { ApprovalState } from 'src/app/models/enums/approval-state.enum';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { RequestsTimeOffsService } from 'src/app/shared/data-access/service/requests-time-offs.service';
import { UserService } from 'src/app/shared/data-access/service/user.service';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup-with-inputs/popup-with-inputs.component';

@Component({
  selector: 'app-request-time-offs',
  templateUrl: './request-time-offs.component.html',
  styleUrls: ['./request-time-offs.component.scss'],
})
export class RequestTimeOffsComponent {
  currentPage$ = new BehaviorSubject<number>(0);
  listOfRequests$: Observable<SharedTableData[]> = this.loadRequests();
  header = ['Name', 'Type', 'From Date', 'To Date', 'Actions'];
  totalNumberOfPages = 1;
  absenceTypeString: string[] = ['Paid Time off', 'Unpaid leave', 'Sick leave'];

  constructor(
    private userService: UserService,
    private requestTimeOffsService: RequestsTimeOffsService,
    private dialog: MatDialog
  ) {}

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }

  openRequestApprovalPopup(guid: string | undefined) {
    if (typeof guid === 'undefined') return;
    const inputs: Dictionary<InputPopupModel> = {};
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Yes',
        onClick: () => this.requestApproval(guid),
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'NO',
      },
    ];

    const data: InputPopupDataModel = {
      title: 'Approve request',
      description: 'Are you sure you want to approve request?',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  openRequestDisapprovalPopup(guid: string | undefined) {
    if (typeof guid === 'undefined') return;
    const inputs: Dictionary<InputPopupModel> = {};
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Yes',
        onClick: () => this.requestDisapproval(guid),
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'NO',
      },
    ];

    const data: InputPopupDataModel = {
      title: 'Disapprove request',
      description: 'Are you sure you want to disapprove request?',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  private requestApproval(requestId: string) {
    this.requestTimeOffsService
      .approveRequest(requestId, ApprovalState.Approved)
      .subscribe(() => (this.listOfRequests$ = this.loadRequests()));
  }

  private requestDisapproval(requestId: string) {
    this.requestTimeOffsService
      .approveRequest(requestId, ApprovalState.Rejected)
      .subscribe(() => (this.listOfRequests$ = this.loadRequests()));
  }

  private loadRequests() {
    return this.userService.user$.pipe(
      switchMap(currentUser =>
        this.requestTimeOffsService.getListOfRequests(currentUser?.id)
      ),
      map(res => {
        this.totalNumberOfPages = res?.page ?? 1;
        if (res?.data.length === 0 && this.currentPage$.value - 1 >= 0)
          this.currentPage$.next(this.currentPage$.value - 1);
        return this.mapData(res);
      })
    );
  }

  private mapData(
    data: PaginatedResult<Absence> | undefined
  ): SharedTableData[] {
    if (typeof data === 'undefined') return [];
    const requests = data.data;
    const results: SharedTableData[] = [];
    requests.forEach(request => {
      const fromDate = new Date(request.fromDate);
      const toDate = new Date(request.toDate);
      const result: SharedTableData = {
        cols: [
          request?.userName ?? '',
          this.absenceTypeString[request.absenceType],
          fromDate.getDay() +
            '/' +
            fromDate.getMonth() +
            '/' +
            fromDate.getFullYear(),
          toDate.getDay() +
            '/' +
            toDate.getMonth() +
            '/' +
            toDate.getFullYear(),
        ],
        actions: [
          {
            icon: 'done',
            func: (arg: string) => {
              this.openRequestApprovalPopup(arg);
            },
            arg: request.id,
          },
          {
            icon: 'close',
            func: (arg: string) => {
              this.openRequestDisapprovalPopup(arg);
            },
            arg: request.id,
          },
        ],
      };
      results.push(result);
    });
    return results;
  }
}
