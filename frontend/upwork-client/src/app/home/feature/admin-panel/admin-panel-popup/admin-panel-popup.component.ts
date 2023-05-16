import { Component, Inject, OnInit } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Dictionary } from 'cypress/types/lodash';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { Roles } from 'src/app/models/enums/roles.enum';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { OrganizationModel } from 'src/app/models/organization.model';
import { RegisterModel } from 'src/app/models/register.model';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { User } from 'src/app/models/user.model';
import { AdminService } from 'src/app/shared/data-access/service/admin.service';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup-with-inputs/popup-with-inputs.component';

@Component({
  selector: 'app-admin-panel-popup',
  templateUrl: './admin-panel-popup.component.html',
  styleUrls: ['./admin-panel-popup.component.scss'],
})
export class AdminPanelPopupComponent implements OnInit {
  public primary = ButtonTypes.PRIMARY;
  header = ['First Name', 'Last Name'];
  users: SharedTableData[] | undefined = [];
  admins: SharedTableData[] | undefined = [];
  owners: SharedTableData[] | undefined = [];
  userRolesString: string[] = ['User', 'Admin', 'Owner'];
  currentPage$ = new BehaviorSubject<number>(0);
  listOfOwners$ = new BehaviorSubject<SharedTableData[]>([]);
  totalNumberOfPages = 1;
  roles = Roles;

  constructor(
    private toatrService: ToastrService,
    private adminService: AdminService,
    private router: Router,
    private dialog: MatDialog,
    public dialogRef: MatDialogRef<AdminPanelPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OrganizationModel
  ) {}

  ngOnInit(): void {
    const guid = this.data.id ?? '';
    this.adminService.getOrganizationWithUsers(guid).subscribe(res => {
      this.owners = this.initOwnersTable(res.users);
      this.admins = this.initAdminsTable(res.users);
      this.users = this.initUsersTable(res.users);
    });
  }

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }

  closePopup() {
    this.dialogRef.close('primary');
  }

  private initOwnersTable(users: User[] | undefined): SharedTableData[] {
    if (typeof users === 'undefined') return [];
    const data: SharedTableData[] = [];
    users
      .filter(x => x.role === Roles.OrganizationOwner)
      .forEach(user => {
        const owner: SharedTableData = {
          cols: [user.firstName ?? '', user.lastName ?? ''],
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
        data.push(owner);
      });
    return data;
  }

  private initAdminsTable(users: User[] | undefined): SharedTableData[] {
    if (typeof users === 'undefined') return [];
    const data: SharedTableData[] = [];
    users
      .filter(x => x.role === Roles.PageAdmin)
      .forEach(user => {
        const admin: SharedTableData = {
          cols: [user.firstName ?? '', user.lastName ?? ''],
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
        data.push(admin);
      });
    return data;
  }

  private initUsersTable(users: User[] | undefined): SharedTableData[] {
    if (typeof users === 'undefined') return [];
    const data: SharedTableData[] = [];
    users
      .filter(x => x.role === Roles.User)
      .forEach(user => {
        const usr: SharedTableData = {
          cols: [user.firstName ?? '', user.lastName ?? ''],
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
        data.push(usr);
      });
    return data;
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

  openCreateUserPopup(userType: Roles): void {
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
      title: `Create ${this.userRolesString[userType]}`,
      description: '',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  private goto(orgUrl: string) {
    // TODO: Go to user's profile
  }

  private createUser(inputs: Dictionary<InputPopupModel>): void {
    return;
  }
}
