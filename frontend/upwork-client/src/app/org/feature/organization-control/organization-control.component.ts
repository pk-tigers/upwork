import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { OrganizationAdminService } from 'src/app/shared/data-access/service/organization-admin.service';
import { PopupWithInputsComponent } from '../../../shared/ui/popup-with-inputs/popup-with-inputs.component';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { BehaviorSubject, Observable, map, switchMap, take, of } from 'rxjs';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { ToastrService } from 'ngx-toastr';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { UserWithSupervisor } from 'src/app/models/user-with-supervisor.model';
import { OrganizationService } from 'src/app/shared/data-access/service/organization.service';
import { RegisterModel } from 'src/app/models/register.model';
import { User } from 'src/app/models/user.model';
import { OrganizationModel } from 'src/app/models/organization.model';

@Component({
  selector: 'app-organization-control',
  templateUrl: './organization-control.component.html',
  styleUrls: ['./organization-control.component.scss'],
})
export class OrganizationControlComponent {
  currentPage$ = new BehaviorSubject<number>(0);
  listOfUsers$: Observable<SharedTableData[]> = this.loadUsersWithSupervisors();
  header = ['First name', 'Last name', 'Current Supervisor', 'Actions'];
  totalNumberOfPages = 1;
  organization: OrganizationModel | undefined;

  constructor(
    private organizationAdminService: OrganizationAdminService,
    private tostr: ToastrService,
    private dialog: MatDialog,
    private organizationService: OrganizationService
  ) {}

  ngOnInit(): void {
    this.organizationService.organization$.subscribe(
      (org: OrganizationModel | null) => {
        if (org) {
          this.organization = org;
        }
      }
    );
  }

  openAddUserPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['firstname']: { value: '', type: 'text', placeholder: 'Firstname' },
      ['lastname']: { value: '', type: 'text', placeholder: 'Lastname' },
      ['email']: { value: '', type: 'text', placeholder: 'E-mail' },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Add user',
        onClick: () => this.addUser(inputs),
      },
      { type: ButtonTypes.SECONDARY, text: 'Cancel' },
    ];

    const data: InputPopupDataModel = {
      title: 'Create user',
      description: 'Fill basic data:',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  addUser(inputs: Dictionary<InputPopupModel>): void {
    this.organizationService.organization$
      .pipe(
        map(organization => organization?.id),
        take(1),
        switchMap(organizationId => {
          const user: RegisterModel = {
            firstName: String(inputs['firstname']?.value),
            lastName: String(inputs['lastname']?.value),
            email: String(inputs['email']?.value),
            organizationId: organizationId || '',
          };

          return this.organizationAdminService.addUser(user);
        })
      )
      .subscribe(res => {
        if (res) {
          this.listOfUsers$ = this.loadUsersWithSupervisors();
          this.tostr.success('Successfully added organization user');
        }
      });
  }

  private loadUsersWithSupervisors() {
    return this.organizationService.organization$.pipe(
      switchMap(org =>
        this.organizationAdminService.getUsersWithSupervisors(org?.id).pipe(
          map((res: PaginatedResult<UserWithSupervisor>) => {
            return this.mapData(res);
          })
        )
      )
    );
  }

  private mapData(
    data: PaginatedResult<UserWithSupervisor>
  ): SharedTableData[] {
    const users = data.data;
    const results: SharedTableData[] = [];
    users.forEach(user => {
      const result: SharedTableData = {
        cols: [
          user.firstName || '',
          user.lastName || '',
          (user.supervisorFirstName || '') +
            ' ' +
            (user.supervisorLastName || '') || 'undefined',
        ],
        actions: [
          {
            icon: 'password',
            func: (arg: string) => {
              this.openResetPasswordPopup();
            },
            arg: user?.id,
          },
          {
            icon: 'lock',
            func: (arg: string) => {
              this.openBlockUserPopup();
            },
            arg: user?.id,
          },
          {
            icon: 'supervisor_account',
            func: (arg: string) => {
              this.openSetSupervisorPopup();
            },
            arg: user?.id,
          },
          {
            icon: 'delete',
            func: (arg: string) => {
              this.openDeleteUserPopup(user);
            },
            arg: user?.id,
          },
        ],
      };
      results.push(result);
    });
    return results;
  }

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }

  openBlockUserPopup() {
    // Logika otwierania popupa dla "Block User"
  }

  openResetPasswordPopup() {
    // Logika otwierania popupa dla "Reset User's Password"
  }

  openSetSupervisorPopup() {
    // Logika otwierania popupa dla "Set Supervisor"
  }

  openDeleteUserPopup(user: UserWithSupervisor): void {
    const inputs: Dictionary<InputPopupModel> = {};
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Delete user',
        onClick: () => {
          if (user.id) {
            this.deleteUser(user.id);
          } else {
            console.log('ups');
          }
        },
      },
      { type: ButtonTypes.SECONDARY, text: 'Cancel' },
    ];

    const data: InputPopupDataModel = {
      title: 'Delete user',
      description:
        'Are you sure you want to delete user: ' +
        user?.firstName +
        ' ' +
        user?.lastName +
        '?',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  deleteUser(guid: string): void {
    this.organizationAdminService
      .deleteUser(guid)
      .pipe(
        switchMap(isDeleted => {
          if (!isDeleted) {
            this.tostr.warning('Something went wrong');
          } else {
            this.tostr.success('User has been deleted');
          }
          return this.loadUsersWithSupervisors();
        })
      )
      .subscribe(updatedUsers => {
        this.listOfUsers$ = of(updatedUsers);
      });
  }

  openOrganizationNameChange() {
    /*
     *
     *
     *
     */
  }
}
