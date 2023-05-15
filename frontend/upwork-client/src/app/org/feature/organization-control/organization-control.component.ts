import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PopupWithInputsComponent } from '../../../shared/ui/popup_with_inputs/popup-with-inputs.component';
import { OrganizationAdminService } from 'src/app/shared/data-access/service/organization-admin.service';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { BehaviorSubject, Observable, map, switchMap } from 'rxjs';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { UserWithSupervisor } from 'src/app/models/user-with-supervisor.model';
import { RegisterModel } from 'src/app/models/register.model';

@Component({
  selector: 'app-organization-control',
  templateUrl: './organization-control.component.html',
  styleUrls: ['./organization-control.component.scss'],
})
export class OrganizationControlComponent {
  currentPage$ = new BehaviorSubject<number>(0);
  listOfUsers$: Observable<SharedTableData[]> = this.loadUsers();
  header = ['First name', 'Last name', 'Current Supervisor', 'Actions'];
  totalNumberOfPages = 1;

  constructor(
    private organizationAdminService: OrganizationAdminService,
    private router: Router,
    private tostr: ToastrService,
    private dialog: MatDialog
  ) {}

  func(arg: string) {
    console.log('func' + arg);
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
        onClick: () => this.addUser(inputs)
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

  /*

  addUser(inputs: Dictionary<InputPopupModel>): void {
    const owner: RegisterModel = {
      firstName: String(inputs['firstname']?.value),
      lastName: String(inputs['lastname']?.value),
      email: String(inputs['email']?.value),
    };
    this.organizationAdminService.addUser(owner).subscribe(res => {
      if (res) {
        this.tostr.success('Successfully added organizations owner');
      }
    });
  }
  */


  addUser(inputs: Dictionary<InputPopupModel>): void {
    //TODO
  }


  private loadUsers(): Observable<SharedTableData[]> {
    return this.currentPage$.pipe(
      switchMap(currentPage => this.organizationAdminService.getUsersWithSupervisors(currentPage)),
      map((res: PaginatedResult<UserWithSupervisor>) => {
        this.totalNumberOfPages = res.page ?? 1;
        if (res.data.length === 0 && this.currentPage$.value - 1 >= 0)
          this.currentPage$.next(this.currentPage$.value - 1);
        return this.mapData(res);
      })
    );
  }

  private mapData(data: PaginatedResult<UserWithSupervisor>): SharedTableData[] {
    const users = data.data;
    const results: SharedTableData[] = [];
    users.forEach(user => {
      const result: SharedTableData = {
        cols: [user.firstName || "", user.lastName || "", ((user.supervisorFirstName || '') + (user.supervisorLastName || '')) || 'undefined'],
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
              this.openDeleteUserPopup(user.id);
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

  openGrantPermissionsPopup() {
    // Logika otwierania popupa dla "Grant Permissions"
  }

  openSetSupervisorPopup() {
    // Logika otwierania popupa dla "Set Supervisor"
  }

/*

  openDeleteUserPopup(user?: UserWithSupervisor): void {
    const inputs: Dictionary<InputPopupModel> = {
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Delete user',
        onClick: () => {
        },
      },
      { type: ButtonTypes.SECONDARY, text: 'Cancel' },
    ];

    const data: InputPopupDataModel = {
      title: 'Delete user',
      description: 'Are you sure you want to delete user: ' + user?.firstName + ' ' + user?.lastName + '?',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  */

  deleteOrganization(guid: string): void {
    this.organizationAdminService.deleteUser(guid).subscribe(isDeleted => {
      if (!isDeleted) this.tostr.warning('Something went wrong');
      else this.listOfUsers$ = this.loadUsers();
    });
  }


  openDeleteUserPopup(guid: string | undefined): void {
    if (typeof guid === 'undefined') return;
    const inputs: Dictionary<InputPopupModel> = {};
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Yes',
        onClick: () => this.deleteUser(),
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'NO',
      },
    ];

    const data: InputPopupDataModel = {
      title: 'Delete organization',
      description: 'Are you sure you want to delete this organization',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }
  deleteUser() {
    throw new Error('Method not implemented.');
  }




  openOrganizationNameChange() {
    /*
     * 
     * 
     * 
     */
  }


}
