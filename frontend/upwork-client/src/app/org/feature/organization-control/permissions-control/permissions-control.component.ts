import { Component } from '@angular/core';
import { Observable, map, switchMap } from 'rxjs';
import { PermissionTypes } from 'src/app/models/enums/permission-types.enum';
import { Roles } from 'src/app/models/enums/roles.enum';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { UpdatePermissions } from 'src/app/models/update-permissions.model';
import { UserWithPermissions } from 'src/app/models/user-with-permissions.model';
import { PermissionsService } from 'src/app/shared/data-access/permissions.service';
import { UserService } from 'src/app/shared/data-access/user.service';

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
    private userService: UserService
  ) {}

  prevPage(): void {
    this.currentPage = this.currentPage - 1;
  }

  nextPage(): void {
    if (this.currentPage < this.totalNumberOfPages - 1)
      this.currentPage = this.currentPage + 1;
  }

  private loadUsersWithPermissions() {
    return this.userService.user$.pipe(
      switchMap(user =>
        this.permissionsService.getUsersWithPermissions(user?.organizationId)
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
      permissionTypes: user.permissionTypes,
    };

    this.permissionsService.updatePermissions(updatePerm).subscribe(res => {
      console.log(res);
    });
  }

  getRoleString(role: Roles): string {
    return Roles[role];
  }

  inputDisabled(role: Roles): boolean {
    return role == Roles.OwneOrganizationOwnerr || role == Roles.PageAdmin;
  }
}
