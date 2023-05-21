import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrganizationModel } from 'src/app/models/organization.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { RegisterModel } from 'src/app/models/register.model';
import { User } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private http: HttpClient) {}

  public createOrganization(
    organization: OrganizationModel
  ): Observable<OrganizationModel> {
    return this.http.post<OrganizationModel>(
      `${environment.apiUrl}/organization`,
      organization
    );
  }

  public getOrganizations(
    pageNumber = 0,
    pageSize = 10
  ): Observable<PaginatedResult<OrganizationModel>> {
    return this.http.get<PaginatedResult<OrganizationModel>>(
      `${environment.apiUrl}/organizations?skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }

  public getOrganizationOwners(
    guid: string,
    pageNumber = 0,
    pageSize = 10
  ): Observable<PaginatedResult<User>> {
    return this.http.get<PaginatedResult<User>>(
      `${
        environment.apiUrl
      }/users/GetOwnersByOrganizationId?organizationId=${guid}&skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }

  public deleteOrganization(guid: string): Observable<boolean> {
    return this.http.delete<boolean>(
      `${environment.apiUrl}/organization/${guid}`
    );
  }

  public createOwner(owner: RegisterModel): Observable<User> {
    return this.http.post<User>(
      `${environment.apiUrl}/user/createOwner`,
      owner
    );
  }

  public deleteUser(userId: string): Observable<boolean> {
    return this.http.delete<boolean>(`${environment.apiUrl}/user?id=${userId}`);
  }
}
