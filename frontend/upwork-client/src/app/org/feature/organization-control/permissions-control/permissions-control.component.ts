import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, map, switchMap } from 'rxjs';
import { PermissionTypes } from 'src/app/models/enums/permission-types.enum';
import { Roles } from 'src/app/models/enums/roles.enum';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { UpdatePermissions } from 'src/app/models/update-permissions.model';
import { UserWithPermissions } from 'src/app/models/user-with-permissions.model';
import { OrganizationService } from 'src/app/shared/data-access/service/organization.service';
import { PermissionsService } from 'src/app/shared/data-access/service/permissions.service';
import { UserService } from 'src/app/shared/data-access/service/user.service';

@Component({
  selector: 'app-permissions-control',
  templateUrl: './permissions-control.component.html',
  styleUrls: [
    './permissions-control.component.scss',
    './../../../../shared/ui/shared-table/shared-table.component.scss',
  ],
})
export class PermissionsControlComponent {
  caption = 'List of all users in organization';
  currentPage = 0;
  totalNumberOfPages = 1;
  listOfUsers$: Observable<PaginatedResult<UserWithPermissions>> =
    this.loadUsersWithPermissions();
  headers = ['Name'];

  enumValues = Object.values(PermissionTypes);
  permissionTypesHeaders = this.enumValues.slice(
    0,
    this.enumValues.length / 2
  ) as string[];
  permissionTypesValues = this.enumValues.slice(
    this.enumValues.length / 2
  ) as PermissionTypes[];

  constructor(
    private permissionsService: PermissionsService,
    private userService: UserService,
    private toastr: ToastrService,
    private organizationService: OrganizationService
  ) {}

  prevPage(): void {
    this.currentPage = this.currentPage - 1;
  }

  nextPage(): void {
    if (this.currentPage < this.totalNumberOfPages - 1)
      this.currentPage = this.currentPage + 1;
  }

  private loadUsersWithPermissions() {
    return this.organizationService.organization$.pipe(
      switchMap(org =>
        this.permissionsService.getUsersWithPermissions(org?.id)
      ),
      map((res: PaginatedResult<UserWithPermissions>) => {
        return res;
      })
    );
  }

  inputSelected(user: UserWithPermissions, permType: PermissionTypes): boolean {
    return user.permissionTypes.includes(permType);
  }
  setInputVal(
    user: UserWithPermissions,
    permType: PermissionTypes,
    newChecked: boolean
  ) {
    if (newChecked && !user.permissionTypes.includes(permType))
      user.permissionTypes.push(permType);
    else if (!newChecked)
      user.permissionTypes = user.permissionTypes.filter(x => x != permType);

    const updatePerm: UpdatePermissions = {
      userId: user.id,
      organizationId: user.organizationId,
      permissionTypes: user.permissionTypes,
    };

    this.permissionsService.updatePermissions(updatePerm).subscribe(res => {
      if (res) {
        this.toastr.success('Permissions changed successfully');
      } else {
        this.toastr.error('Failed to change permissions');
      }
    });
  }

  getRoleString(role: Roles): string {
    return Roles[role];
  }

  inputDisabled(role: Roles): boolean {
    return role == Roles.OrganizationOwner || role == Roles.PageAdmin;
  }
}
