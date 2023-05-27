import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Dictionary } from 'cypress/types/lodash';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable, map, switchMap } from 'rxjs';
import { TooltipTexts } from 'src/app/models/enums/tooltips-types.enum';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { OrganizationModel } from 'src/app/models/organization.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { SharedTableData } from 'src/app/models/shared-table-data.model';
import { AdminService } from 'src/app/shared/data-access/service/admin.service';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup-with-inputs/popup-with-inputs.component';
import { AdminPanelPopupComponent } from './admin-panel-popup/admin-panel-popup.component';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss'],
})
export class AdminPanelComponent {
  currentPage$ = new BehaviorSubject<number>(0);
  listOfOrganization$: Observable<SharedTableData[]> = this.loadOrganizations();
  header = ['Organizations name', 'UrlName', 'Actions'];
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
      ['UrlName']: {
        value: '',
        type: 'text',
        placeholder: "Your organization's url",
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

  openManageOrganizationPopup(organization: OrganizationModel): void {
    this.dialog.open(AdminPanelPopupComponent, { data: organization });
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
      description: 'Are you sure you want to delete this organization?',
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
      urlName: String(inputs['UrlName'].value)
        .replace(/\s/g, '-')
        .replace('.', '-'),
    };
    this.adminService.createOrganization(organization).subscribe(() => {
      this.listOfOrganization$ = this.loadOrganizations();
    });
  }

  deleteOrganization(guid: string): void {
    this.adminService.deleteOrganization(guid).subscribe(isDeleted => {
      if (!isDeleted) this.tostr.warning('Something went wrong');
      else this.listOfOrganization$ = this.loadOrganizations();
    });
  }

  async goTo(urlName: string | undefined): Promise<void> {
    if (typeof urlName === 'undefined') return;
    await this.router.navigate([`/org/${urlName}/calendar`]); //TODO change to dashboard
  }

  setPage(pageNumber: number): void {
    this.currentPage$.next(pageNumber);
  }

  private loadOrganizations(): Observable<SharedTableData[]> {
    return this.currentPage$.pipe(
      switchMap(currentPage => this.adminService.getOrganizations(currentPage)),
      map((res: PaginatedResult<OrganizationModel>) => {
        this.totalNumberOfPages = res.page ?? 1;
        if (res.data.length === 0 && this.currentPage$.value - 1 >= 0)
          this.currentPage$.next(this.currentPage$.value - 1);
        return this.mapData(res);
      })
    );
  }

  private mapData(data: PaginatedResult<OrganizationModel>): SharedTableData[] {
    const organizations = data.data;
    const results: SharedTableData[] = [];
    organizations.forEach(organization => {
      const result: SharedTableData = {
        cols: [organization.name, organization.urlName],
        actions: [
          {
            icon: 'settings',
            func: (arg: OrganizationModel) => {
              this.openManageOrganizationPopup(arg);
            },
            arg: organization,
            tooltip: TooltipTexts.organizationSettings,
          },
          {
            icon: 'delete',
            func: (arg: string) => {
              this.openDeleteOrganizationPopup(arg);
            },
            arg: organization?.id,
            tooltip: TooltipTexts.deleteOrganization,
          },
          {
            icon: 'launch',
            func: async (arg: string) => {
              await this.goTo(arg);
            },
            arg: organization.urlName,
            tooltip: TooltipTexts.organizationCalendar,
          },
        ],
      };
      results.push(result);
    });
    return results;
  }
}
