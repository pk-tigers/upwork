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
import { BehaviorSubject, Observable, map, switchMap, take, of, mergeMap } from 'rxjs';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { ToastrService } from 'ngx-toastr';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { UserWithSupervisor } from 'src/app/models/user-with-supervisor.model';
import { UpdateSupervisor } from 'src/app/models/update-supervisor.model';
import { OrganizationService } from 'src/app/shared/data-access/service/organization.service';
import { SupervisorService } from 'src/app/shared/data-access/service/supervisor.service';
import { RegisterModel } from 'src/app/models/register.model';
import { User } from 'src/app/models/user.model';
import { OrganizationModel } from 'src/app/models/organization.model';
import { first } from 'cypress/types/lodash';


@Component({
  selector: 'app-organization-control',
  templateUrl: './organization-control.component.html',
  styleUrls: ['./organization-control.component.scss'],
})
export class OrganizationControlComponent {
  currentPage$ = new BehaviorSubject<number>(0);
  listOfUsers$: Observable<SharedTableData[]> = this.loadUsersWithSupervisors();
  listOfSupervisors: User[] = [];
  header = ['First name', 'Last name', 'Current Supervisor', 'Actions'];
  totalNumberOfPages = 1;
  organization: OrganizationModel | undefined;

  constructor(
    private organizationAdminService: OrganizationAdminService,
    private tostr: ToastrService,
    private dialog: MatDialog,
    private organizationService: OrganizationService,
    private supervisorService: SupervisorService,
  ) {}

    ngOnInit(): void {
      this.getSupervisors();
      this.organizationService.organization$.subscribe((org: OrganizationModel | null) => {
        if (org) {
          this.organization = org;
        }
      });

  }

  private loadUsersWithSupervisors() {
    return this.organizationService.organization$.pipe(
      switchMap(org =>
        this.organizationAdminService.getUsersWithSupervisors(org?.id).pipe(
          map((res: PaginatedResult<UserWithSupervisor>) => {
            return  this.mapData(res);
          })
        )
      )
    );
  }

  getSupervisors(): void {
    this.organizationService.organization$.pipe(
      switchMap(org => this.supervisorService.getSupervisors(org?.id)),
      take(1)
    ).subscribe((result: PaginatedResult<User>) => {
      this.listOfSupervisors = result.data;
    });
    this.sortSupervisorsByFirstNameAndLastName(this.listOfSupervisors);
  }

  sortSupervisorsByFirstNameAndLastName(users: User[]): void {
    users.sort((a, b) => {
      const firstNameComparison = (a.firstName || '').localeCompare(b.firstName || '');
      if (firstNameComparison !== 0) {
        return firstNameComparison;
      }
      return (a.lastName || '').localeCompare(b.lastName || '');
    });
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


addUser(inputs: Dictionary<InputPopupModel>): void {
  this.organizationService.organization$.pipe(
    map(organization => organization?.id),
    take(1),
    switchMap(organizationId => {
      const user: RegisterModel = {
        firstName: String(inputs['firstname']?.value),
        lastName: String(inputs['lastname']?.value),
        email: String(inputs['email']?.value),
        organizationId: organizationId || ''
      };
      
      return this.organizationAdminService.addUser(user);
    })
  ).subscribe(res => {
    if (res) {
      this.listOfUsers$ = this.loadUsersWithSupervisors();
      this.tostr.success('Successfully added organization user');
    }
  });
}


  private mapData(data: PaginatedResult<UserWithSupervisor>): SharedTableData[] {
    const users = data.data;
    const results: SharedTableData[] = [];
    users.forEach(user => {
      const result: SharedTableData = {
        cols: [user.firstName || "", user.lastName || "", ((user.supervisorFirstName || '') + " " + (user.supervisorLastName || '')) || 'undefined'],
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
              this.openSetSupervisorPopup(user);
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


  openSetSupervisorPopup(user: UserWithSupervisor): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['SupervisorsOptions']: {
        value: '',
        type: 'select',
        placeholder: 'Select new supervisor',
        selectOptions: this.listOfSupervisors.map(obj => ({
          value: obj.id || '', 
          displayValue: `${obj.firstName} ${obj.lastName}`,
        })),      
      },
    };

    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Set',
        onClick: () => this.setSupervisor(inputs, user),
      },
    ];

    const data: InputPopupDataModel = {
      title: 'Set new supervisor for ' + user.firstName + ' ' + user.lastName,
      description: 'Fill data:',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }


  setSupervisor(inputs: Dictionary<InputPopupModel>, user: UserWithSupervisor) {
    const updateSupervisor: UpdateSupervisor = {};
  
    this.organizationService.organization$.pipe(
      map(organization => organization?.id),
      switchMap(organizationId => {
        updateSupervisor.organizationId = organizationId;
        updateSupervisor.userId = user.id;
        updateSupervisor.newSupervisorId = inputs['SupervisorsOptions'].value?.toString();
        return this.supervisorService.updateUserSupervisor(updateSupervisor);
      }),
      take(1)
    ).subscribe(updatedSupervisor => {
      if (updatedSupervisor) {
        this.tostr.success('Supervisor has been updated');
      } else {
        this.tostr.warning('Something went wrong');
      }
      this.listOfUsers$ = this.loadUsersWithSupervisors();
    });

  }


  openDeleteUserPopup(user: UserWithSupervisor): void {
    const inputs: Dictionary<InputPopupModel> = {
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Delete user',
        onClick: () => {
          if(user.id) {
            this.deleteUser(user.id);
          }
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


  deleteUser(guid: string): void {
    this.organizationAdminService.deleteUser(guid).pipe(
      switchMap(isDeleted => {
        if (!isDeleted) {
          this.tostr.warning('Something went wrong');
        } else {this.tostr.success('User has been deleted');}
        return this.loadUsersWithSupervisors();
      }),
      take(1)
    ).subscribe(updatedUsers => {
      this.listOfUsers$ = of(updatedUsers);
    });
  }
  
  //TODO: Logika otwierania popupa dla "Block User"
  openBlockUserPopup() {

  }

  //TODO: Logika otwierania popupa dla "Reset User's Password"
  openResetPasswordPopup() {
    
  }


}
