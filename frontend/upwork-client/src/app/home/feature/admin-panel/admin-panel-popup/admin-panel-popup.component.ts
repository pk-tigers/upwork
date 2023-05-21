import { Component, Inject, OnInit } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Dictionary } from 'cypress/types/lodash';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable, map, switchMap } from 'rxjs';
import { Roles } from 'src/app/models/enums/roles.enum';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { OrganizationModel } from 'src/app/models/organization.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { RegisterModel } from 'src/app/models/register.model';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { User } from 'src/app/models/user.model';
import { AdminService } from 'src/app/shared/data-access/service/admin.service';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup-with-inputs/popup-with-inputs.component';
import { EmailCreator } from 'src/app/shared/web-utilities/email-creator';

@Component({
  selector: 'app-admin-panel-popup',
  templateUrl: './admin-panel-popup.component.html',
  styleUrls: ['./admin-panel-popup.component.scss'],
})
export class AdminPanelPopupComponent {
  public primary = ButtonTypes.PRIMARY;
  organizationId = '';
  header = ['First Name', 'Last Name'];
  currentPage$ = new BehaviorSubject<number>(0);
  listOfOwners$: Observable<SharedTableData[]> = this.loadOwners();
  totalNumberOfPages = 1;

  constructor(
    private toatrService: ToastrService,
    private adminService: AdminService,
    private router: Router,
    private dialog: MatDialog,
    public dialogRef: MatDialogRef<AdminPanelPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OrganizationModel
  ) {
    this.organizationId = this.data.id ?? '';
  }

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }

  closePopup() {
    this.dialogRef.close('primary');
  }

  openDeleteUserPopup(guid: string | undefined): void {
    if (typeof guid === 'undefined') return;
    const inputs: Dictionary<InputPopupModel> = {};
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Yes',
        onClick: () => this.deleteUser(guid),
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'NO',
      },
    ];

    const data: InputPopupDataModel = {
      title: 'Delete user',
      description: 'Are you sure you want to delete this user',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  private deleteUser(userId: string): void {
    this.adminService.deleteUser(userId).subscribe(isDeleted => {
      if (isDeleted) this.toatrService.success('Successfully deleted');
      else this.toatrService.error('Something went wrong');
    });
  }

  openCreateUserPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['firstName']: {
        value: '',
        type: 'text',
        placeholder: 'first name',
      },
      ['lastName']: {
        value: '',
        type: 'text',
        placeholder: 'last name',
      },
    };

    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Yes',
        onClick: () => {
          this.createUser(inputs);
        },
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'NO',
      },
    ];

    const data: InputPopupDataModel = {
      title: `Create Owner`,
      description: '',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  private createUser(inputs: Dictionary<InputPopupModel>): void {
    const firstName = String(inputs['firstName'].value);
    const lastName = String(inputs['lastName'].value);
    const owner: RegisterModel = {
      firstName: firstName,
      lastName: lastName,
      email: EmailCreator.createEmail(firstName, lastName, this.data.name),
      organizationId: this.organizationId,
    };
    this.adminService.createOwner(owner).subscribe();
  }

  private goto(userId: string) {
    // TODO: Go to user's profile
  }

  private loadOwners(): Observable<SharedTableData[]> {
    return this.currentPage$.pipe(
      switchMap(currentPage =>
        this.adminService.getOrganizationOwners(
          this.organizationId,
          currentPage
        )
      ),
      map((res: PaginatedResult<User>) => {
        this.totalNumberOfPages = res?.page ?? 1;
        if (res?.data.length === 0 && this.currentPage$.value - 1 >= 0)
          this.currentPage$.next(this.currentPage$.value - 1);
        return this.mapData(res);
      })
    );
  }

  private mapData(data: PaginatedResult<User>) {
    const users = data.data;
    const owners: SharedTableData[] = [];
    users.forEach(user => {
      const owner: SharedTableData = {
        cols: [String(user.firstName), String(user.lastName)],
        actions: [
          {
            icon: 'delete',
            func: (arg: string) => {
              this.openDeleteUserPopup(arg);
            },
            arg: user.id,
          },
          {
            icon: 'launch',
            func: (arg: string) => {
              this.goto(arg);
            },
            arg: user.id,
          },
        ],
      };
      owners.push(owner);
    });
    return owners;
  }
}
