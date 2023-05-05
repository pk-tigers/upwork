import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Dictionary } from 'cypress/types/lodash';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable, map, switchMap } from 'rxjs';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { OrganizationModel } from 'src/app/models/organization.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { RegisterModel } from 'src/app/models/register.model';
import { AdminService } from 'src/app/shared/data-access/admin.service';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup_with_inputs/popup-with-inputs.component';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss'],
})
export class AdminPanelComponent {
  currentPage$ = new BehaviorSubject<number>(0);
  listOfOrganization$: Observable<OrganizationModel[]> =
    this.loadOrganizations();
  totalNumberOfPages = 1;

  constructor(
    private adminService: AdminService,
    private router: Router,
    private tostr: ToastrService,
    private dialog: MatDialog
  ) {}

  openCreateOrganizationPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['OrganizationName']: {
        value: '',
        type: 'text',
        placeholder: "Your organization's name",
      },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Create Organization',
        onClick: () => this.createOrganization(inputs),
      },
    ];
    const data: InputPopupDataModel = {
      title: 'Create new organization',
      description: '',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  openManageOrganizationPopup(guid: string | undefined): void {
    if (typeof guid === 'undefined') return;
    const inputs: Dictionary<InputPopupModel> = {
      ['firstname']: { value: '', type: 'text', placeholder: 'Firstname' },
      ['lastname']: { value: '', type: 'text', placeholder: 'Lastname' },
      ['email']: { value: '', type: 'text', placeholder: 'E-mail' },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Create user',
        onClick: () => this.manageOrganization(inputs, guid),
      },
      { type: ButtonTypes.SECONDARY, text: 'Cancel' },
    ];

    const data: InputPopupDataModel = {
      title: 'Add owner',
      description: 'Fill basic data:',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  openDeleteOrganizationPopup(guid: string | undefined): void {
    if (typeof guid === 'undefined') return;
    const inputs: Dictionary<InputPopupModel> = {};
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Yes',
        onClick: () => this.deleteOrganization(guid),
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

  createOrganization(inputs: Dictionary<InputPopupModel>): void {
    const organization: OrganizationModel = {
      name: String(inputs['OrganizationName'].value),
      urlName: String(inputs['OrganizationName'].value)
        .replace(/\s/g, '-')
        .replace('.', '-'),
    };
    this.adminService.createOrganization(organization).subscribe(() => {
      this.listOfOrganization$ = this.loadOrganizations();
    });
  }

  manageOrganization(inputs: Dictionary<InputPopupModel>, orgId: string): void {
    const owner: RegisterModel = {
      firstName: String(inputs['firstname']?.value),
      lastName: String(inputs['lastname']?.value),
      email: String(inputs['email']?.value),
      organizationId: orgId,
    };
    this.adminService.createUser(owner).subscribe(res => {
      if (res) {
        this.tostr.success('Successfully added organizations owner');
      }
    });
  }

  deleteOrganization(guid: string): void {
    this.adminService.deleteOrganization(guid).subscribe(isDeleted => {
      if (!isDeleted) this.tostr.warning('Something went wrong');
      else this.listOfOrganization$ = this.loadOrganizations();
    });
  }

  goTo(urlName: string | undefined): void {
    if (typeof urlName === 'undefined') return;
    // TODO: route to correct url
    // this.router.navigate([`/${urlName}/dashboard`]);
  }

  nextPage(): void {
    if (this.currentPage$.value < this.totalNumberOfPages - 1) {
      this.currentPage$.next(this.currentPage$.value + 1);
    }
  }

  prevPage(): void {
    if (this.currentPage$.value > 0) {
      this.currentPage$.next(this.currentPage$.value - 1);
    }
  }

  private loadOrganizations() {
    return this.currentPage$.pipe(
      switchMap(currentPage => this.adminService.getOrganizations(currentPage)),
      map((res: PaginatedResult<OrganizationModel>) => {
        this.totalNumberOfPages = res.page ?? 1;
        if (res.data.length === 0) this.prevPage();
        return res.data;
      })
    );
  }
}
