import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { UpdatePermissions } from 'src/app/models/update-permissions.model';
import { UserWithPermissions } from 'src/app/models/user-with-permissions.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PermissionsService {
  private env = environment;
  constructor(private http: HttpClient) {}

  public getUsersWithPermissions(
    organizationId?: string,
    pageNumber = 0,
    pageSize = 10
  ): Observable<PaginatedResult<UserWithPermissions>> {
    return this.http.get<PaginatedResult<UserWithPermissions>>(
      `${
        this.env.apiUrl
      }/users/LoadUsersWithPermissions?organizationId=${organizationId}&skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }

  public updatePermissions(updatePermissionsDto: UpdatePermissions) {
    return this.http.post<boolean>(
      `${this.env.apiUrl}/permissions/UpdatePermissions`,
      updatePermissionsDto
    );
  }
}
