import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Observable, map, switchMap } from 'rxjs';
import { Absence } from 'src/app/models/absence.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { ApprovalState } from 'src/app/models/enums/approval-state.enum';
import { PermissionTypes } from 'src/app/models/enums/permission-types.enum';
import { TooltipTexts } from 'src/app/models/enums/tooltips-types.enum';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import {
  SharedTableData,
  SharedTableDataFunc,
} from 'src/app/models/shared-table-data.model';
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
  currentPageHistory$ = new BehaviorSubject<number>(0);
  listOfRequests$: Observable<SharedTableData[]> = this.loadRequests();
  listOfRequestsHistory$: Observable<SharedTableData[]> =
    this.loadRequestsHistory();
  header = ['Name', 'Type', 'From Date', 'To Date', 'Actions'];
  headerHistory = [
    'Name',
    'Type',
    'From Date',
    'To Date',
    'Request State',
    'Change Status',
  ];
  totalNumberOfPages = 1;
  totalNumberOfPagesHistory = 1;
  absenceTypeString: string[] = [
    'Paid Time off',
    'Unpaid leave',
    'Sick leave',
    'Maternity leave',
  ];
  absenceStateString: string[] = ['Pending', 'Approved', 'Rejected'];
  permissionTypes = PermissionTypes;

  constructor(
    private userService: UserService,
    private requestTimeOffsService: RequestsTimeOffsService,
    private dialog: MatDialog
  ) {}

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }

  setPageHistory(pageNumber: number): void {
    this.currentPageHistory$.next(pageNumber);
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
      .subscribe(() => {
        this.listOfRequests$ = this.loadRequests();
        this.listOfRequestsHistory$ = this.loadRequestsHistory();
      });
  }

  private requestDisapproval(requestId: string) {
    this.requestTimeOffsService
      .approveRequest(requestId, ApprovalState.Rejected)
      .subscribe(() => {
        this.listOfRequests$ = this.loadRequests();
        this.listOfRequestsHistory$ = this.loadRequestsHistory();
      });
  }

  private loadRequests() {
    return this.currentPage$.pipe(
      switchMap(currentPage =>
        this.requestTimeOffsService.getListOfRequests(currentPage)
      ),
      map(res => {
        this.totalNumberOfPages = res?.page ?? 1;
        if (res?.data.length === 0 && this.currentPage$.value - 1 >= 0)
          this.currentPage$.next(this.currentPage$.value - 1);
        return this.mapData(res);
      })
    );
  }

  private loadRequestsHistory() {
    return this.currentPageHistory$.pipe(
      switchMap(currentPage =>
        this.requestTimeOffsService.getListOfRequestsHistory(currentPage)
      ),
      map(res => {
        this.totalNumberOfPagesHistory = res?.page ?? 1;
        if (res?.data.length === 0 && this.currentPageHistory$.value - 1 >= 0)
          this.currentPageHistory$.next(this.currentPageHistory$.value - 1);
        return this.mapDataHistory(res);
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
          request?.userFirstName?.toString() +
            ' ' +
            request?.userLastName?.toString(),
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
            tooltip: TooltipTexts.acceptRequest,
          },
          {
            icon: 'close',
            func: (arg: string) => {
              this.openRequestDisapprovalPopup(arg);
            },
            arg: request.id,
            tooltip: TooltipTexts.cancelRequest,
          },
        ],
      };
      results.push(result);
    });
    return results;
  }

  private mapDataHistory(
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
          request?.userFirstName?.toString() +
            ' ' +
            request?.userLastName?.toString(),
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
          this.absenceStateString[request.approvalState],
        ],
        actions: this.getActionChangeRequestOffState(request),
      };
      results.push(result);
    });
    return results;
  }

  private getActionChangeRequestOffState(
    request: Absence
  ): SharedTableDataFunc[] {
    if (request.approvalState === ApprovalState.Approved) {
      return [
        {
          icon: 'close',
          func: (arg: string) => {
            this.openRequestDisapprovalPopup(arg);
          },
          arg: request.id,
        },
      ];
    } else if (request.approvalState == ApprovalState.Rejected) {
      return [
        {
          icon: 'done',
          func: (arg: string) => {
            this.openRequestApprovalPopup(arg);
          },
          arg: request.id,
        },
      ];
    }
    return [];
  }
}
